using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using Library.Generics.Query.QueryModels.StudioGame;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using Library.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StudioGameService.DB.Model;
using StudioGameService.Repository;
using StudioGameService.Repository.Interfaces;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Services
{
    public class StudioService : IStudioService
    {
        private readonly IStudioRepository studioRepository;

        private readonly Mongo mongoService;
        private readonly IMapper mapper;

        const string imgsDatabase = "ImagesDatabase";
        const string avasCollection = "GameCollection";

        const string contentDatabase = "ImagesDatabase";
        const string contentCollection = "ContentCollection";

        public StudioService(IStudioRepository studioRepository, Mongo mongoService, IMapper mapper)
        {
            this.studioRepository = studioRepository;
            this.mongoService = mongoService;
            this.mapper = mapper;
        }

        public async Task AddAsync(StudioDTO studioDto)
        {
            var studioModel = mapper.Map<Studio>(studioDto);

            try
            {
                await studioRepository.BeginTransactionAsync();

                await studioRepository.AddAsync(studioModel);
                await studioRepository.SaveChangesAsync();

                if (studioDto.Image != null && studioDto.ImageType != null)
                {
                    await mongoService.Database(imgsDatabase)
                        .Collection(avasCollection)
                        .InsertImg(studioModel.Id, studioDto.Image, studioDto.ImageType);
                }

                await mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .InsertStrContent(studioModel.Id, studioDto.Content);


                await studioRepository.CommitTransactionAsync();

                studioDto.Id = studioModel.Id;
            }
            catch (Exception ex)
            {
                await studioRepository.RollbackTransactionAsync();
                await CompensateAddAsync(studioModel.Id); // Очищаем MongoDB
                Console.WriteLine($"Ошибка при добавлении студии: {ex.Message}");
                throw;
            }
        }

        private async Task CompensateAddAsync(Guid studioId)
        {
            try
            {
                await mongoService.Database(imgsDatabase).Collection(avasCollection)
                    .Delete(studioId);

                await mongoService.Database(contentDatabase).Collection(contentCollection)
                    .Delete(studioId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при откате изменений: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var studio = await studioRepository.GetByIdAsync(id);
            if (studio == null) return;

            try
            {
                await studioRepository.BeginTransactionAsync();

                await studioRepository.DeleteAsync(studio);
                await studioRepository.SaveChangesAsync();

                await mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .Delete(id);

                await mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .Delete(id);

                await studioRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await studioRepository.RollbackTransactionAsync();
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<StudioDTO>> GetAllAsync()
        {
            var studios = await studioRepository.GetAllAsync();

            if (!studios.Any())
                return Enumerable.Empty<StudioDTO>();

            var tasks = studios.Select(studio => MapToStudioDTOAsync(studio));
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        private async Task<StudioDTO> MapToStudioDTOAsync(Studio studio)
        {
            if (studio == null) return null;

            try
            {
                var imageTask = mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .GetImgById(studio.Id);

                var contentTask = mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .GetContentById(studio.Id);

                var imageResult = await imageTask;
                var contentResult = await contentTask;

                var dto = mapper.Map<StudioDTO>(studio);

                if (imageResult != null)
                {
                    dto.Image = imageResult.Bytes;
                    dto.ImageType = imageResult.ContentType;
                }
                else
                {
                    dto.Image = null;
                    dto.ImageType = null;
                }

                dto.Content = contentResult?.Value;

                return dto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке студии с ID {studio.Id}: {ex.Message}");
                return null;
            }
        }

        public async Task<StudioDTO> GetByIdAsync(Guid id)
        {
            var studio = await studioRepository.GetByIdAsync(id);
            if (studio == null) return null;

            return await MapToStudioDTOAsync(studio);
        }

        public async Task UpdateAsync(StudioDTO studio)
        {
            if (studio == null)
                throw new ArgumentNullException(nameof(studio));

            await studioRepository.BeginTransactionAsync();

            try
            {
                var existingGame = await studioRepository.GetByIdAsync(studio.Id);
                if (existingGame == null)
                    throw new KeyNotFoundException($"Игра с ID {studio.Id} не найдена.");

                mapper.Map(studio, existingGame);
                await studioRepository.UpdateAsync(existingGame);
                await studioRepository.SaveChangesAsync();

                // Обновляем изображение в MongoDB: Delete + Insert
                if (!string.IsNullOrEmpty(studio.Image))
                {
                    var imageBytes = studio.Image;

                    await mongoService.Database(imgsDatabase)
                        .Collection(avasCollection)
                        .Delete(studio.Id);

                    await mongoService.Database(imgsDatabase)
                        .Collection(avasCollection)
                        .InsertImg(studio.Id, imageBytes, studio.ImageType);
                }

                // Обновляем контент в MongoDB: Delete + Insert
                if (!string.IsNullOrEmpty(studio.Content))
                {
                    await mongoService.Database(contentDatabase)
                        .Collection(contentCollection)
                        .Delete(studio.Id);

                    await mongoService.Database(contentDatabase)
                        .Collection(contentCollection)
                        .InsertStrContent(studio.Id, studio.Content);
                }

                await studioRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await studioRepository.RollbackTransactionAsync();
                Console.WriteLine($"Ошибка при обновлении студии: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<StudioDTO>> GetFiltreCardStudioAsync(ParamQueryStudio paramQueryListStudio)
        {
            var result = await studioRepository.GetFiltreCardStudioAsync(paramQueryListStudio);

            var tasks = result.Select(studio => MapToStudioDTOAsync(studio));
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        public async Task<IEnumerable<Studio>> GetStudioByUserIdAsync(Guid id)
        {
            return await studioRepository.GetStudioByUserIdAsync(id);
        }
    }
}

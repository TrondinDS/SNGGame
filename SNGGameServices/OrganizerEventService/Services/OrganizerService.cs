using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OrganizerEventService.DB.Models;
using OrganizerEventService.Repository.Interfaces;
using OrganizerEventService.Services.Interfaces;

namespace OrganizerEventService.Services
{
    public class OrganizerService : IOrganizerService
    {
        protected readonly IOrganizerRepository repository;
        private readonly Mongo mongoService;
        private readonly IMapper mapper;

        const string imgsDatabase = "ImagesDatabase";
        const string avasCollection = "GameCollection";

        const string contentDatabase = "ImagesDatabase";
        const string contentCollection = "GameContentCollection";

        public OrganizerService(IOrganizerRepository repository, Mongo mongoService, IMapper mapper)
        {
            this.repository = repository;
            this.mongoService = mongoService;
            this.mapper = mapper;
        }

        public async Task AddAsync(OrganizerDTO dto)
        {
            var model = mapper.Map<Organizer>(dto);
            try
            {
                await repository.BeginTransactionAsync();

                await mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .InsertImg(model.Id, dto.Image, dto.ImageType);

                await mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .InsertStrContent(model.Id, dto.Content);

                await repository.AddAsync(model);
                await repository.SaveChangesAsync();
                await repository.CommitTransactionAsync();

                dto.Id = model.Id;
            }
            catch (Exception ex)
            {
                await repository.RollbackTransactionAsync();
                await CompensateAddAsync(model.Id);
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }
        private async Task CompensateAddAsync(Guid organizerId)
        {
            try
            {
                await mongoService.Database(imgsDatabase).Collection(avasCollection)
                    .Delete(organizerId);

                await mongoService.Database(contentDatabase).Collection(contentCollection)
                    .Delete(organizerId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при откате изменений: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var organizer = await repository.GetByIdAsync(id);
            if (organizer == null) return;

            try
            {
                await repository.BeginTransactionAsync();

                await repository.DeleteAsync(organizer);
                await repository.SaveChangesAsync();

                await mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .Delete(id);

                await mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .Delete(id);

                await repository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await repository.RollbackTransactionAsync();
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<OrganizerDTO>> GetAllAsync()
        {
            var organizers = await repository.GetAllAsync();

            if (!organizers.Any())
                return Enumerable.Empty<OrganizerDTO>();

            var tasks = organizers.Select(organizer => ModelToDTO(organizer));
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        public async Task<OrganizerDTO> GetByIdAsync(Guid id)
        {
            var organizer = await repository.GetByIdAsync(id);
            if (organizer == null) return null;

            return await ModelToDTO(organizer);
        }

        public async Task UpdateAsync(OrganizerDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            await repository.BeginTransactionAsync();

            try
            {
                var existingOrganizer = await repository.GetByIdAsync(dto.Id);
                if (existingOrganizer == null)
                    throw new KeyNotFoundException($"Organizer with ID {dto.Id} not found.");

                mapper.Map(dto, existingOrganizer);
                await repository.UpdateAsync(existingOrganizer);
                await repository.SaveChangesAsync();

                await mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .InsertImg(dto.Id, dto.Image, dto.ImageType);
                await mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .InsertStrContent(dto.Id, dto.Content);

                await repository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await repository.RollbackTransactionAsync();
                Console.WriteLine($"Error updating organizer: {ex.Message}");
                throw;
            }
        }


        private async Task<OrganizerDTO> ModelToDTO(Organizer organizer)
        {
            if (organizer == null) return null;

            try
            {
                var imageTask = mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .GetImgById(organizer.Id);

                var contentTask = mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .GetContentById(organizer.Id);

                await Task.WhenAll(imageTask, contentTask);

                var imageResult = imageTask.Result;
                var contentResult = contentTask.Result;

                var dto = mapper.Map<OrganizerDTO>(organizer);

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
                Console.WriteLine($"Error processing organizer with ID {organizer.Id}: {ex.Message}");
                return null;
            }
        }
    }
}

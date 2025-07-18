﻿using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelView.StudioGameService.Game;
using Library.Generics.Query.QueryModels.StudioGame;
using Library.Services;
using MongoDB.Driver;
using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Services
{
    public class GameService : IGameService
    {
        protected readonly IGameRepository gameRepository;
        private readonly Mongo mongoService;
        private readonly IMapper mapper;

        const string imgsDatabase = "ImagesDatabase";
        const string avasCollection = "GameCollection";

        const string contentDatabase = "ImagesDatabase";
        const string contentCollection = "ContentCollection";

        public GameService(IGameRepository gameRepository, Mongo mongoService, IMapper mapper)
        {
            this.gameRepository = gameRepository;
            this.mongoService = mongoService;
            this.mapper = mapper;
        }

        public async Task AddAsync(GameDTO game)
        {
            var gameModel = mapper.Map<Game>(game);

            try
            {
                await gameRepository.BeginTransactionAsync(); // Начинаем транзакцию

                await gameRepository.AddAsync(gameModel);
                await gameRepository.SaveChangesAsync(); // Сохраняем в рамках транзакции

                await mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .InsertImg(gameModel.Id, game.Image, game.ImageType);

                await mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .InsertStrContent(gameModel.Id, game.Content);

                await gameRepository.CommitTransactionAsync(); // Фиксируем изменения

                game.Id = gameModel.Id;
            }
            catch (Exception ex)
            {
                await gameRepository.RollbackTransactionAsync(); // Откатываем Postgres
                await CompensateAddAsync(gameModel.Id); // Очищаем MongoDB
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }

        private async Task CompensateAddAsync(Guid gameId)
        {
            try
            {
                await mongoService.Database(imgsDatabase).Collection(avasCollection)
                    .Delete(gameId);

                await mongoService.Database(contentDatabase).Collection(contentCollection)
                    .Delete(gameId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при откате изменений: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var game = await gameRepository.GetByIdAsync(id);
            if (game == null) return;

            try
            {
                await gameRepository.BeginTransactionAsync();

                await gameRepository.DeleteAsync(game);
                await gameRepository.SaveChangesAsync();

                await mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .Delete(id);

                await mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .Delete(id);

                await gameRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await gameRepository.RollbackTransactionAsync();
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<GameDTO>> GetAllAsync()
        {
            var games = await gameRepository.GetAllAsync();

            if (!games.Any())
                return Enumerable.Empty<GameDTO>();

            var tasks = games.Select(game => MapToGameDTOAsync(game));
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        private async Task<GameDTO> MapToGameDTOAsync(Game game)
        {
            if (game == null) return null;

            try
            {
                var imageTask = mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .GetImgById(game.Id);

                var contentTask = mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .GetContentById(game.Id);

                await Task.WhenAll(imageTask, contentTask);

                var imageResult = imageTask.Result;
                var contentResult = contentTask.Result;

                var dto = mapper.Map<GameDTO>(game);

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
                Console.WriteLine($"Ошибка при обработке игры с ID {game.Id}: {ex.Message}");
                return null;
            }
        }

        public async Task<GameDTO> GetByIdAsync(Guid id)
        {
            var game = await gameRepository.GetByIdAsync(id);
            if (game == null) return null;

            return await MapToGameDTOAsync(game);
        }

        public async Task UpdateAsync(GameDTO game)
        {
            if (game == null)
                throw new ArgumentNullException(nameof(game));

            await gameRepository.BeginTransactionAsync();

            try
            {
                var existingGame = await gameRepository.GetByIdAsync(game.Id);
                if (existingGame == null)
                    throw new KeyNotFoundException($"Игра с ID {game.Id} не найдена.");

                mapper.Map(game, existingGame);
                await gameRepository.UpdateAsync(existingGame);
                await gameRepository.SaveChangesAsync();

                // Обновляем изображение в MongoDB: Delete + Insert
                if (!string.IsNullOrEmpty(game.Image))
                {
                    var imageBytes = game.Image;

                    await mongoService.Database(imgsDatabase)
                        .Collection(avasCollection)
                        .Delete(game.Id);

                    await mongoService.Database(imgsDatabase)
                        .Collection(avasCollection)
                        .InsertImg(game.Id, imageBytes, game.ImageType);
                }

                // Обновляем контент в MongoDB: Delete + Insert
                if (!string.IsNullOrEmpty(game.Content))
                {
                    await mongoService.Database(contentDatabase)
                        .Collection(contentCollection)
                        .Delete(game.Id);

                    await mongoService.Database(contentDatabase)
                        .Collection(contentCollection)
                        .InsertStrContent(game.Id, game.Content);
                }

                await gameRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await gameRepository.RollbackTransactionAsync();
                Console.WriteLine($"Ошибка при обновлении игры: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<GameDTO>> FilterGame(ParamQueryGame paramQuerySG)
        {
            var games = await gameRepository.GetFilterGame(paramQuerySG);
            return await MapToGameDTOsAsync(games);
        }

        private async Task<IEnumerable<GameDTO>> MapToGameDTOsAsync(IEnumerable<Game> games)
        {
            if (!games.Any())
                return Enumerable.Empty<GameDTO>();

            var tasks = games.Select(MapToGameDTOAsync);
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        public async Task<IEnumerable<StatisticGame>> GetStatisticGames(IEnumerable<Guid> listGameId)
        {
            return await gameRepository.GetStatisticGames(listGameId);
        }

        public async Task<IEnumerable<Game>> GetAllCardGameAsync()
        {
            return await gameRepository.GetAllCardGameAsync();
        }

        public async Task<IEnumerable<Game>> GetSelectCardGameAsync(IEnumerable<Guid> idGames)
        {
            return await gameRepository.GetSelectCardGameAsync(idGames);
        }

        public async Task<IEnumerable<Game>> GetFiltreCardGameAsync(ParamQueryGame paramQuerySG)
        {
            return await gameRepository.GetFiltreCardGameAsync(paramQuerySG);
        }

        public async Task<IEnumerable<GameDTOView>> GetGameDTOViewByIdGamesAsync(IEnumerable<Guid> listGameId)
        {
            // Получаем из репозитория базовые данные GameDTOView (или похожие)
            var result = await gameRepository.GetGameDTOViewByIdGamesAsync(listGameId);

            // Получаем полный список GameDTO с Image, ImageType, Content
            var resultGameIMG = await MapToGameDTOsAsync(result);

            // Мапим исходный результат к GameDTOView
            var resultDto = mapper.Map<List<GameDTOView>>(result); // Сохраняем в List, чтобы можно было изменять

            // Создаем словарь для быстрого поиска по Id из resultGameIMG
            var gameImgDict = resultGameIMG.ToDictionary(x => x.Id);

            // Проходимся по результату и подставляем Image, ImageType, Content из resultGameIMG
            foreach (var dtoView in resultDto)
            {
                if (gameImgDict.TryGetValue(dtoView.Game.Id, out var gameImg))
                {
                    dtoView.Game.Image = gameImg.Image;
                    dtoView.Game.ImageType = gameImg.ImageType;
                    dtoView.Game.Content = gameImg.Content;
                }
            }

            return resultDto;
        }
    }
}

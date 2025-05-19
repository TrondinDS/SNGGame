using AutoMapper;
using Library.Generics.DB.DTO.DTOModelObjects.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.Query.QueryModels.StudioGame;
using Library.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
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

        public GameService(IGameRepository gameRepository, Mongo mongoService)
        {
            this.gameRepository = gameRepository;
            this.mongoService = mongoService;
        }

        public async Task AddAsync(GameDTO game)
        {
            var gameModel = mapper.Map<Game>(game);
            try
            {
                await gameRepository.AddAsync(gameModel);
                await gameRepository.SaveChangesAsync();

                // TODO(kra53n): also send image contentType from game
                string avaImgContentType = "png";
                await mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .InsertImg(gameModel.Id, game.Image, avaImgContentType);

                await mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .InsertStrContent(gameModel.Id, game.Content);
            }
            catch (Exception ex)
            {
                await CompensateAddAsync(gameModel.Id);
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
            }

            try
            {
                // Удаляем из Postgres
                await DeleteAsync(gameId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка удаления игры из БД: {ex.Message}");
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var game = await gameRepository.GetByIdAsync(id);
            if (game != null)
            {
                gameRepository.DeleteAsync(game);
                await gameRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Game>> GetAllAsync()
        {
            return gameRepository.GetAllAsync();
        }

        public async Task<Game> GetByIdAsync(Guid id)
        {
            return await gameRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Game game)
        {
            gameRepository.UpdateAsync(game);
            await gameRepository.SaveChangesAsync();
        }




















        public async Task<IEnumerable<Game>> FilterGame(ParamQueryGame paramQuerySG)
        {
            return await gameRepository.GetFilterGame(paramQuerySG);
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

        public async Task<IEnumerable<StatisticGame>> GetStatisticGames(IEnumerable<Guid> listGameId)
        {
            return await gameRepository.GetStatisticGames(listGameId);
        }
    }
}

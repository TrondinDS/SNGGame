using Library.Generics.DB.DTO.DTOModelObjects.Game;
using Library.Generics.Query.QueryModels.StudioGame;
using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Services
{
    public class GameService : IGameService
    {
        protected readonly IGameRepository gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            this.gameRepository = gameRepository;
        }

        public async Task AddAsync(Game game)
        {
            await gameRepository.AddAsync(game);
            await gameRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
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

        public async Task<Game> GetByIdAsync(int id)
        {
            return await gameRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Game game)
        {
            gameRepository.UpdateAsync(game);
            await gameRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Game>> FilterGame(ParamQuerySG paramQuerySG)
        {
            return await gameRepository.GetFilterGame(paramQuerySG);
        }

        public async Task<IEnumerable<Game>> GetAllCardGameAsync()
        {
            return await gameRepository.GetAllCardGameAsync();
        }

        public async Task<IEnumerable<Game>> GetSelectCardGameAsync(IEnumerable<int> idGames)
        {
            return await gameRepository.GetSelectCardGameAsync(idGames);
        }

        public async Task<IEnumerable<Game>> GetFiltreCardGameAsync(ParamQuerySG paramQuerySG)
        {
            return await gameRepository.GetFiltreCardGameAsync(paramQuerySG);
        }
    }
}

using Library.Generics.DB.DTO.DTOModelObjects.Game;
using Library.Generics.Query.QueryModels.StudioGame;
using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IGameService
    {
        Task AddAsync(Game game);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<Game>> GetAllAsync();
        Task<Game> GetByIdAsync(Guid id);
        Task UpdateAsync(Game game);

        Task<IEnumerable<Game>> FilterGame(ParamQueryGame paramQuerySG);

        public Task<IEnumerable<Game>> GetAllCardGameAsync();
        public Task<IEnumerable<Game>> GetSelectCardGameAsync(IEnumerable<Guid> idGames);
        public Task<IEnumerable<Game>> GetFiltreCardGameAsync(ParamQueryGame paramQuerySG);

        public Task<IEnumerable<StatisticGame>> GetStatisticGames(IEnumerable<Guid> listGameId);
    }
}

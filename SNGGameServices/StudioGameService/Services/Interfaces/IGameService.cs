using Library.Generics.DB.DTO.DTOModelObjects.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelView.StudioGameService.Game;
using Library.Generics.Query.QueryModels.StudioGame;
using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IGameService
    {
        Task AddAsync(GameDTO game);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<GameDTO>> GetAllAsync();
        Task<GameDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(GameDTO game);

        Task<IEnumerable<GameDTO>> FilterGame(ParamQueryGame paramQuerySG);

        public Task<IEnumerable<Game>> GetAllCardGameAsync();
        public Task<IEnumerable<Game>> GetSelectCardGameAsync(IEnumerable<Guid> idGames);
        public Task<IEnumerable<Game>> GetFiltreCardGameAsync(ParamQueryGame paramQuerySG);

        public Task<IEnumerable<StatisticGame>> GetStatisticGames(IEnumerable<Guid> listGameId);
        public Task<IEnumerable<GameDTOView>> GetGameDTOViewByIdGamesAsync(IEnumerable<Guid> listGameId);
    }
}

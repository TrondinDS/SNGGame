using Library.Generics.DB.DTO.DTOModelView.StudioGameService.Game;
using Library.Generics.GenericRepository.Interfaces;
using Library.Generics.Query.QueryModels.StudioGame;
using StudioGameService.DB.Model;

namespace StudioGameService.Repository.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game, Guid> 
    {
        public Task<IEnumerable<Game>> GetFilterGame(ParamQueryGame paramQuerySG);

        public Task<IEnumerable<Game>> GetAllCardGameAsync();
        public Task<IEnumerable<Game>> GetSelectCardGameAsync(IEnumerable<Guid> idGames);
        public Task<IEnumerable<Game>> GetFiltreCardGameAsync(ParamQueryGame paramQuerySG);
        
        public Task<IEnumerable<StatisticGame>> GetStatisticGames(IEnumerable<Guid> listGameId);
        public Task<IEnumerable<Game>> GetGameDTOViewByIdGamesAsync(IEnumerable<Guid> listGameId);
    }
}

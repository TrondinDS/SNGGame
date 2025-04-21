using Library.Generics.DB.DTO.DTOModelObjects.Game;
using Library.Generics.GenericRepository.Interfaces;
using Library.Generics.Query.QueryModels.StudioGame;
using StudioGameService.DB.Model;

namespace StudioGameService.Repository.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game, int> 
    {
        public Task<IEnumerable<Game>> GetFilterGame(ParamQueryGame paramQuerySG);

        public Task<IEnumerable<Game>> GetAllCardGameAsync();
        public Task<IEnumerable<Game>> GetSelectCardGameAsync(IEnumerable<int> idGames);
        public Task<IEnumerable<Game>> GetFiltreCardGameAsync(ParamQueryGame paramQuerySG);
    }
}
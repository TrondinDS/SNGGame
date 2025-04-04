using Library.Generics.GenericRepository.Interfaces;
using Library.Generics.Query.QueryModels.StudioGame;
using StudioGameService.DB.Model;

namespace StudioGameService.Repository.Interfaces
{
    public interface IGameRepository : IGenericRepository<Game, int> 
    {
        public Task<IEnumerable<Game>> GetFilterGame(ParamQuerySG paramQuerySG);
    }
}
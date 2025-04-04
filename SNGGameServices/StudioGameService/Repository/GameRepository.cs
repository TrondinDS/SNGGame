using Library.Generics.GenericRepository;
using Library.Generics.Query.QueryModels.StudioGame;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;

namespace StudioGameService.Repository
{
    public class GameRepository : GenericRepository<Game, int>, IGameRepository
    {
        private readonly DbSet<Game> dbSet;
        public GameRepository(ApplicationContext context)
            : base(context) 
        {
            dbSet = context.Set<Game>();
        }

        public IEnumerable<Game> GetFilterGame(ParamQuerySG paramQuerySG)
        {
            var query = dbSet.AsQueryable();

        }
    }
}

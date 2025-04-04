using Library.Generics.GenericRepository;
using Library.Generics.Query.QueryModels.StudioGame;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Filter;
using StudioGameService.Repository.Interfaces;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Game>> GetFilterGame(ParamQuerySG paramQuerySG)
        {
            var query = dbSet.AsQueryable();

            query = FilterQuery.CreateQueryble(paramQuerySG, query);
            var result = await query.ToListAsync();

            return result;
        }
    }
}

using Library.Generics.GenericRepository;
using Library.Generics.Query.QueryModels.StudioGame;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Filter;
using StudioGameService.Repository.Interfaces;

namespace StudioGameService.Repository
{
    public class StudioRepository : GenericRepository<Studio, int>, IStudioRepository
    {
        private readonly DbSet<Studio> dbSet;
        public StudioRepository(ApplicationContext context)
            : base(context) 
        {
            dbSet = context.Set<Studio>();
        }

        public async Task<IEnumerable<Studio>> GetFiltreCardStudioAsync(ParamQuerySG paramQuerySG)
        {
            var query = dbSet
                .AsQueryable();

            query = FilterQueryStudio.CreateQuerybleAsNoTraking(paramQuerySG, query);

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Studio>> GetFiltreCardStudioAsync(QueryListStudio paramQueryListStudio)
        {
            var query = dbSet
                .AsQueryable();

            query = FilterQueryStudio.CreateQueryableStudioListAsNoTraking(paramQueryListStudio, query);

            var result = await query.ToListAsync();

            return result;
        }
    }
}

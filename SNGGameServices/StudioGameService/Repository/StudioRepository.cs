using Library.Generics.GenericRepository;
using Library.Generics.Query.QueryModels.StudioGame;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Filter;
using StudioGameService.Repository.Interfaces;

namespace StudioGameService.Repository
{
    public class StudioRepository : GenericRepository<Studio, Guid>, IStudioRepository
    {
        private readonly DbSet<Studio> dbSet;
        public StudioRepository(ApplicationContext context)
            : base(context)
        {
            dbSet = context.Set<Studio>();
        }

        public async Task<IEnumerable<Studio>> GetFiltreCardStudioAsync(ParamQueryStudio paramQueryListStudio)
        {
            var query = dbSet
                .AsQueryable();

            query = FilterQueryStudio.CreateQuerybleAsNoTraking(paramQueryListStudio, query);

            var result = await query.ToListAsync();

            return result;
        }

        public async Task<IEnumerable<Studio>> GetStudioByUserIdAsync(Guid id)
        {
            var result = await dbSet.Where(s => s.OwnerId == id).ToListAsync();

            return result;
        }
    }
}

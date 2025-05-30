using Library.Generics.GenericRepository;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Model;
using UserService.DB.Context;
using UserService.Repository.Interfaces;

namespace UserService.Repository
{
    public class JobRepository : GenericRepository<Job, int>, IJobRepository
    {
        private DbSet<Job> dbSet;
        public JobRepository(ApplicationContext context)
            : base(context) 
        {
            dbSet = context.Set<Job>();
        }

        public async Task<IEnumerable<Job>> GetJobsByUserIdAsync(Guid id)
        {
            var result = await dbSet.Where(j => j.UserId == id && j.DateFinish > DateTime.UtcNow).ToListAsync();
            return result;
        }
    }
}

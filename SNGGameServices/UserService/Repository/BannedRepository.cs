using BannedService.DB.Models;
using Library.Generics.GenericRepository;
using Microsoft.EntityFrameworkCore;
using UserService.DB.Context;
using UserService.Repository.Interfaces;

namespace UserService.Repository
{
    public class BannedRepository : GenericRepository<Banned, Guid>, IBannedRepository
    {
        private DbSet<Banned> dbSetBanned;
        public BannedRepository(ApplicationContext context)
            : base(context) 
        {
            dbSetBanned = context.Set<Banned>();
        }

        public async Task<IEnumerable<Banned>> GetBannedsByUserIdAsync(Guid userId)
        {
            return await dbSetBanned.Where(b => b.UserIdBanned == userId).ToListAsync();
        }
    }
}

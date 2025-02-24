using BannedService.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace BannedService.DB.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext) : base(dbContext) 
        {
        }
        DbSet<Banned> Banneds { get; set; }
    }
}

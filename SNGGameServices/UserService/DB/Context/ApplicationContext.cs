using BannedService.DB.Models;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Model;
using UserService.DB.Models;

namespace UserService.DB.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext) : base (dbContext) 
        {
        }

        DbSet<User> Users { get; set; }
        DbSet<UserSubscription> Subscriptions { get; set; }
        DbSet<Banned> Banneds { get; set; }
        DbSet<Job> Jobs { get; set; }

    }
}

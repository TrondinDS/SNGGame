using BannedService.DB.Models;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Model;
using UserService.DB.Models;

namespace UserService.DB.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext)
            : base(dbContext) { }

        DbSet<User> Users { get; set; }
        DbSet<UserSubscription> Subscriptions { get; set; }
        DbSet<Banned> Banneds { get; set; }
        DbSet<Job> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Job>()
                .HasOne(job => job.User)
                .WithMany(user => user.Jobs)
                .HasForeignKey(job => job.UserId);

            modelBuilder
                .Entity<UserSubscription>()
                .HasOne(us => us.User)
                .WithMany(user => user.Subscriptions)
                .HasForeignKey(us => us.UserId);

            modelBuilder
                .Entity<Banned>()
                .HasOne(ban => ban.UserModerator)
                .WithMany(user => user.ModeratedBans)
                .HasForeignKey(ban => ban.UserIdModerator);

            modelBuilder
                .Entity<Banned>()
                .HasOne(ban => ban.UserBanned)
                .WithMany(user => user.BannedRecords)
                .HasForeignKey(ban => ban.UserIdBanned);
        }
    }
}

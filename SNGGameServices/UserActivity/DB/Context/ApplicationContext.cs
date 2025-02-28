using Microsoft.EntityFrameworkCore;
using UserActivityService.DB.Models;

namespace UserActivityService.DB.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext) : base(dbContext)
        {
        }
        DbSet<Comment> Comments { get; set; }
        DbSet<Topic> Topics { get; set; }
        DbSet<UserReaction> UserReactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserReaction>()
                .HasOne(ur => ur.Comment)
                .WithMany(c => c.UserReactions)
                .HasForeignKey(ur => ur.CommentId);

            modelBuilder.Entity<Comment>()
                .HasOne(c => c.Topic)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TopicId);
        }
    }
}

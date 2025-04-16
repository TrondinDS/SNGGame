using Library.Generics.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using UserActivityService.DB.Models;

namespace UserActivityService.DB.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext)
            : base(dbContext) { }

        DbSet<Comment> Comments { get; set; }
        DbSet<Topic> Topics { get; set; }
        DbSet<UserReaction> UserReactions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new InterceptorOverrideDelete());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<Topic>().HasQueryFilter(x => x.IsDeleted == false);

            var dateTimeUtcConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime) || property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(dateTimeUtcConverter);
                    }
                }
            }

            modelBuilder
                .Entity<UserReaction>()
                .HasOne(ur => ur.Comment)
                .WithMany(c => c.UserReactions)
                .HasForeignKey(ur => ur.CommentId);

            modelBuilder
                .Entity<Comment>()
                .HasOne(c => c.Topic)
                .WithMany(t => t.Comments)
                .HasForeignKey(c => c.TopicId);
        }
    }
}

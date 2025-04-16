using Library.Generics.Interceptors;
using Microsoft.EntityFrameworkCore;
using OrganizerEventService.DB.Models;

namespace OrganizerEventService.DB.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext)
            : base(dbContext) { }

        DbSet<Event> Events { get; set; }
        DbSet<Organizer> EventOrganizers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new InterceptorOverrideDelete());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Event>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<Organizer>().HasQueryFilter(x => x.IsDeleted == false);

            // Event -> EventOrganizer
            modelBuilder
                .Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(eo => eo.Events)
                .HasForeignKey(e => e.OrganizerEventId);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using OrganizerEventService.DB.Models;

namespace OrganizerEventService.DB.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext) : base(dbContext)
        {
        }
        DbSet<Event> Events { get; set; }
        DbSet<EventOrganizer> EventOrganizers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Event -> EventOrganizer
            modelBuilder.Entity<Event>()
                .HasOne(e => e.Organizer)
                .WithMany(eo => eo.Events)
                .HasForeignKey(e => e.OrganizerEventId);
        }
    }
}

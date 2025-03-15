using AdministratumService.DB.Models;
using Microsoft.EntityFrameworkCore;

namespace AdministratumService.DB.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext)
            : base(dbContext) { }

        DbSet<ChatFeedback> ChatFeedbacks { get; set; }
        DbSet<ComplainTicket> ComplainTickets { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<UserComplains> UserComplains { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder
                .Entity<Message>()
                .HasOne(m => m.ChatFeedback)
                .WithMany(cf => cf.Messages)
                .HasForeignKey(m => m.ChatFeedbackId);

            modelBuilder
                .Entity<UserComplains>()
                .HasOne(uc => uc.Ticket)
                .WithMany(t => t.UserComplains)
                .HasForeignKey(uc => uc.TicketId);
        }
    }
}

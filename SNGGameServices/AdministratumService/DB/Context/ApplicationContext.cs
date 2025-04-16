using AdministratumService.DB.Models;
using Library.Generics.Interceptors;
using Microsoft.EntityFrameworkCore;

namespace AdministratumService.DB.Context
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext)
            : base(dbContext) { }

        public DbSet<ChatFeedback> ChatFeedbacks { get; set; }
        public DbSet<ComplainTicket> ComplainTickets { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<UserComplains> UserComplains { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.AddInterceptors(new InterceptorOverrideDelete());
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ComplainTicket>().HasQueryFilter(x => x.IsDeleted == false);
            modelBuilder.Entity<Message>().HasQueryFilter(x => x.IsDeleted == false);

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

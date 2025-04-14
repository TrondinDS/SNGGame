using GetAwaitService.DB.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GetAwaitService.DB.Context
{
    public class ApplicationContext
    : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> dbContext)
            : base(dbContext) { }

        DbSet<UserTelegramInformation> UserTelegramInformations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            base.OnModelCreating(modelBuilder);
        }
    }
}

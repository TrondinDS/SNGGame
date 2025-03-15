using Microsoft.EntityFrameworkCore;
using OrganizerEventService.DB.Context;

namespace OrganizerEventService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddSingleton<Library.Services.Mongo>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                return new Library.Services.Mongo(
                    config.GetConnectionString("UserServiceMongoConnection")
                );
            });

            builder.Services.AddDbContext<ApplicationContext>(opt =>
                opt.UseNpgsql(
                    builder.Configuration.GetConnectionString("OrganizerEventServiceConnection")
                )
            );

            var app = builder.Build();

            // ���������� ��������
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

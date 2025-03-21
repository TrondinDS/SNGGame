using AdministratumService.DB.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.SwaggerGen;
using AdministratumService.DB.Models;
using AdministratumService.Repository;
using Library.GenericService;

namespace AdministratumService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddScoped<Repository.ChatFeedbackRepository>();
            builder.Services.AddScoped<CrudGenericService<ChatFeedback, int, ChatFeedbackRepository>>();
            builder.Services.AddScoped<Repository.ComplainTicketRepository>();
            builder.Services.AddScoped<CrudGenericService<ComplainTicket, int, ComplainTicketRepository>>();

            builder.Services.AddDbContext<ApplicationContext>(opt =>
                opt.UseNpgsql(
                    builder.Configuration.GetConnectionString("AdministratumServiceConnection")
                )
            );

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                //dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            
            if (app.Environment.IsDevelopment())
            {
                // Swagger UI
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty; // Swagger URL
                });
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}

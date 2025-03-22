using Microsoft.EntityFrameworkCore;
using OrganizerEventService.DB.Context;
using Swashbuckle.AspNetCore.SwaggerUI;
using Swashbuckle.AspNetCore.SwaggerGen;
using Library.Generics.GenericService;
using OrganizerEventService.DB.Models;
using OrganizerEventService.Repository;

namespace OrganizerEventService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            builder.Services.AddSingleton<Library.Services.Mongo>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                return new Library.Services.Mongo(
                    config.GetConnectionString("MongoConnection")
                );
            });

            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            {
                builder.Services.AddScoped<EventRepository>();
                builder.Services.AddScoped<CrudGenericService<Event, int, EventRepository>>();
 
                builder.Services.AddScoped<OrganizerRepository>();
                builder.Services.AddScoped<CrudGenericService<Organizer, int, OrganizerRepository>>();
            }

            builder.Services.AddDbContext<ApplicationContext>(opt =>
                opt.UseNpgsql(
                    builder.Configuration.GetConnectionString("OrganizerEventServiceConnection")
                )
            );

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                dbContext.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseCors("AllowAllOrigins");

                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty; 
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

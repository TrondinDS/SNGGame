using Microsoft.EntityFrameworkCore;
using UserService.DB.Context;
using UserService.Repository;
using UserService.Repository.Interfaces;
using UserService.Services;
using UserService.Services.Interfaces;

namespace UserService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddSingleton<Library.Services.Mongo>(provider =>
            {
                var config = provider.GetRequiredService<IConfiguration>();
                return new Library.Services.Mongo(
                    config.GetConnectionString("UserServiceMongoConnection")
                );
            });

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddTransient<IUserRepository, UserRepository>();
            builder.Services.AddTransient<IUserService, UserServiceS>();

            builder.Services.AddTransient<IUserSubscriptionRepository,UserSubscriptionRepository>();
            builder.Services.AddTransient<IUserSubscriptionService, UserSubscriptionService>();

            builder.Services.AddTransient<IJobRepository, JobRepository>();
            builder.Services.AddTransient<IJobService, JobService>();

            builder.Services.AddTransient<IBannedRepository, BannedRepository>();
            builder.Services.AddTransient<IBannedService, BannedServiceS>();

            builder.Services.AddDbContext<ApplicationContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("UserServiceConnection"))
            );

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins", policy =>
                {
                    policy.AllowAnyOrigin() // Allow requests from any origin
                          .AllowAnyMethod()  // Allow any HTTP method (GET, POST, etc.)
                          .AllowAnyHeader(); // Allow any headers
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
                app.MapOpenApi();
            }

            // Configure the HTTP request pipeline.
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

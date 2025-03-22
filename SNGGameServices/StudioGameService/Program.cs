using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Context;
using StudioGameService.Repository;
using StudioGameService.Repository.Interfaces;
using StudioGameService.Services;
using StudioGameService.Services.Interfaces;
using StudioGenreService.Services;
using TagGameService.Services;

namespace StudioGameService
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

            builder.Services.AddTransient<IGameRepository, GameRepository>();
            builder.Services.AddTransient<IGameService, GameService>();

            builder.Services.AddTransient<IGameLibraryRepository, GameLibraryRepository>();
            builder.Services.AddTransient<IGameLibraryService, GameLibraryService>();

            builder.Services.AddTransient<IGameSelectedGenreRepository,GameSelectedGenreRepository>();
            builder.Services.AddTransient<IGameSelectedGenreService, GameSelectedGenreService>();

            builder.Services.AddTransient<IGameSelectedTagRepository, GameSelectedTagRepository>();
            builder.Services.AddTransient<IGameSelectedTagService, GameSelectedTagService>();

            builder.Services.AddTransient<IGenreRepository, GenreRepository>();
            builder.Services.AddTransient<IGenreService, GenreService>();

            builder.Services.AddTransient<IStudioRepository, StudioRepository>();
            builder.Services.AddTransient<IStudioService, StudioService>();

            builder.Services.AddTransient<ITagRepository, TagRepository>();
            builder.Services.AddTransient<ITagService, TagService>();

            builder.Services.AddDbContext<ApplicationContext>(opt =>
                opt.UseNpgsql(
                    builder.Configuration.GetConnectionString("StudioGameServiceConnection")
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
                //dbContext.Database.Migrate();
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

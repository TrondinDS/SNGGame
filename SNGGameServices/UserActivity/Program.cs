using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using UserActivityService.DB.Context;
using UserActivityService.Repository;
using UserActivityService.Repository.Interfaces;
using UserActivityService.Services;
using UserActivityService.Services.Interfaces;

namespace UserActivity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddTransient<ICommentRepository, CommentRepository>();
            builder.Services.AddTransient<ICommentService, CommentService>();

            builder.Services.AddTransient<ITopicRepository, TopicRepository>();
            builder.Services.AddTransient<ITopicService, TopicService>();

            builder.Services.AddTransient<IUserReactionRepository, UserReactionRepository>();
            builder.Services.AddTransient<IUserReactionService, UserReactionService>();

            builder.Services.AddDbContext<ApplicationContext>(opt =>
                opt.UseNpgsql(
                    builder.Configuration.GetConnectionString("UserActivityServiceConnection")
                )
            );

            var app = builder.Build();

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

            if (app.Environment.IsDevelopment())
            {
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

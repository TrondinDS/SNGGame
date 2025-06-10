using AdministratumService.DB.Context;
using Microsoft.EntityFrameworkCore;
using AdministratumService.Repository;
using AdministratumService.Repository.Interfaces;
using AdministratumService.Services.Interfaces;
using AdministratumService.Services;

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

            {
                builder.Services.AddTransient<IChatFeedbackRepository, ChatFeedbackRepository>();
                builder.Services.AddTransient<IChatFeedbackService, ChatFeedbackService>();

                builder.Services.AddTransient<IComplainTicketRepository, ComplainTicketRepository>();
                builder.Services.AddTransient<IComplainTicketService, ComplainTicketService>();
            }

            builder.Services.AddDbContext<ApplicationContext>(opt =>
                opt.UseNpgsql(
                    builder.Configuration.GetConnectionString("AdministratumServiceConnection")
                )
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

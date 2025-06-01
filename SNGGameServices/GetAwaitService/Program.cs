using GetAwaitService.Auth.JWT.Service;
using GetAwaitService.DB.Context;
using GetAwaitService.Repository;
using GetAwaitService.Services.GetAwaitService;
using GetAwaitService.Services.GetAwaitService.Interfaces;
using GetAwaitService.Services.UserService.Interfaces;
using GetAwaitService.Services.UserService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using GetAwaitService.Services.UserActivityService.Interfaces;
using GetAwaitService.Services.UserActivityService;
using GetAwaitService.Services.StudioGameService.Interfaces;
using GetAwaitService.Services.StudioGameService;
using GetAwaitService.Services.UserAccessRightsService.Interfaces;
using GetAwaitService.Services.UserAccessRightsService;

namespace GetAwaitService
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAutoMapper(typeof(Program));
            builder.Services.AddControllers();

            // Swagger
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });

                // Добавляем определение безопасности (Bearer JWT)
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter 'Bearer' [space] and then your token in the text input below."
                });

                // Добавляем требование безопасности (глобальное)
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            // Настройка аутентификации (JWT)
            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = builder.Configuration["Jwt:Issuer"],
                    ValidAudience = builder.Configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            // Регистрация сервисов
            builder.Services.AddTransient<IAuthServiceJWT, AuthServiceJWT>();

            builder.Services.AddTransient<IUserTelegramInformationRepository, UserTelegramInformationRepository>();
            builder.Services.AddTransient<IUserTelegramInformationService, UserTelegramInformationService>();

            // Настройка HTTP-клиентов
            AddNamedHttpClient(builder.Services, "UserServiceClient", "https://userservices:8081");
            AddNamedHttpClient(builder.Services, "UserActivityServiceClient", "https://user-activity-service:8081");
            AddNamedHttpClient(builder.Services, "StudioGameServiceClient", "https://studio-game-service:8081");

            // Добавление сервисов для контроллеров
            builder.Services.AddScoped<IUserApiService, UserApiService>();
            builder.Services.AddScoped<IJobApiService, JobApiService>();
            builder.Services.AddScoped<IUserSubscriptionApiService, UserSubscriptionApiService>();
            builder.Services.AddScoped<IBannedApiService, BannedApiService>();

            builder.Services.AddScoped<ICommentApiService, CommentApiService>();
            builder.Services.AddScoped<ITopicApiService, TopicApiService>();
            builder.Services.AddScoped<IUserReactionApiService, UserReactionApiService>();

            builder.Services.AddScoped<IGameService, GameApiService>();
            builder.Services.AddScoped<IGameLibraryService, GameLibraryApiService>();
            builder.Services.AddScoped<IGameSelectedGenreService, GameSelectedGenreApiService>();
            builder.Services.AddScoped<IGameSelectedTagService, GameSelectedTagApiService>();
            builder.Services.AddScoped<IGenreService, GenreApiService>();
            builder.Services.AddScoped<IStudioService, StudioApiService>();
            builder.Services.AddScoped<ITagService, TagApiService>();

            builder.Services.AddScoped<IUserAccessRightsService, UserAccessRightsApiService>();


            builder.Services.AddDbContext<ApplicationContext>(opt =>
                opt.UseNpgsql(builder.Configuration.GetConnectionString("UserTelegramInformationServiceConnection"))
            );

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationContext>();
                dbContext.Database.Migrate();
            }

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                    c.RoutePrefix = string.Empty; // Отключаем префикс "/swagger"
                });
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        static void AddNamedHttpClient(IServiceCollection services, string name, string baseAddress)
        {
            services.AddHttpClient(name, client =>
            {
                client.BaseAddress = new Uri(baseAddress);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                handler.ServerCertificateCustomValidationCallback =
                    HttpClientHandler.DangerousAcceptAnyServerCertificateValidator;
                return handler;
            });
        }
    }
}
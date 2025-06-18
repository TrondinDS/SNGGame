using FrontRazor.Services.GameService;
using FrontRazor.Services.StudioService;
using FrontRazor.Services.UserService;
using FrontService.Services;
using FrontService.Services.FrontService.Services.CommentService;
using FrontService.Services.Interfaces;
using FrontService.Services.TopicService;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;

namespace FrontService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Razor Pages
            builder.Services.AddRazorPages();

            // ��������� ������ � HttpContext
            builder.Services.AddHttpContextAccessor();

            // ��������� HTTP-������ � ������������ �� JWT �� ����
            AddNamedHttpClient(builder.Services, "GetAwaitClient", "https://get-await-service:8081");

            // DI-�������
            builder.Services.AddScoped<IUserApiService, UserApiService>();
            builder.Services.AddScoped<IStudioApiService, StudioApiService>();
            builder.Services.AddScoped<IGameApiService, GameApiService>();
            builder.Services.AddScoped<IGenreApiService, GenreApiService>();
            builder.Services.AddScoped<ITagApiService, TagApiService>();
            builder.Services.AddScoped<ITopicApiService, TopicApiService>();
            builder.Services.AddScoped<ICommentApiService, CommentApiService>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            var app = builder.Build();

            // Middleware pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthorization(); // ����� ����� �������� UseAuthentication, ���� ����� ������ � [Authorize]

            app.MapStaticAssets();
            app.MapRazorPages().WithStaticAssets();

            app.Run();
        }

        /// <summary>
        /// ����������� ����������� HttpClient, ������� ������������� ��������� ����� �� ���� � Authorization-���������
        /// </summary>
        static void AddNamedHttpClient(IServiceCollection services, string name, string baseAddress)
        {
            services.AddHttpClient(name, (sp, client) =>
            {
                var httpContextAccessor = sp.GetRequiredService<IHttpContextAccessor>();
                var token = httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];

                if (!string.IsNullOrEmpty(token))
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                client.BaseAddress = new Uri(baseAddress);
            })
            .ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                };
                return handler;
            });
        }
    }
}

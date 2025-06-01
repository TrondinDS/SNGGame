using GetAwaitService.Services.UserService.Interfaces;
using Library.Types;

namespace GetAwaitService.Services.UserAccessRightsService
{
    public class BannedUserMiddleware
    {
        private readonly RequestDelegate _next;

        public BannedUserMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, IServiceProvider serviceProvider)
        {
            // Получаем IBannedApiService уже внутри обработки запроса
            var bannedApiService = serviceProvider.GetRequiredService<IBannedApiService>();

            // Проверяем, авторизован ли пользователь и можем ли мы получить UserId
            if (context.User.Identity?.IsAuthenticated == true &&
                    Guid.TryParse(context.User.FindFirst("userId")?.Value, out var userId))
            {
                try
                {
                    var bansInfo = await bannedApiService.GetBannedsByUserId(userId);

                    var activeBans = bansInfo
                        .Where(b => b.TypePunishment == (int)PunishmentType.Type.Ban &&
                                    b.DateFinish > DateTime.UtcNow)
                        .ToList();

                    if (activeBans.Any())
                    {
                        var longestBan = activeBans
                            .OrderByDescending(b => b.DateFinish)
                            .First();

                        context.Response.StatusCode = StatusCodes.Status403Forbidden;
                        context.Response.ContentType = "application/json";

                        var errorResponse = new
                        {
                            error = "Ваш аккаунт пользователя заблокирован",
                            reason = $"Причина блокировки : {longestBan.Reason}",
                            bannedUntil = longestBan.DateFinish
                        };

                        await context.Response.WriteAsJsonAsync(errorResponse);
                        return;
                    }
                }
                catch (Exception ex)
                {
                    context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                    await context.Response.WriteAsJsonAsync(new
                    {
                        error = "Ошибка сервера при проверке ограничений"
                    });
                    return;
                }
            }

            await _next(context);
        }
    }
}

using FrontService.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Login
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _authService;

        public LoginModel(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<IActionResult> OnGetAsync(long? telegramId)
        {
            if (telegramId == null)
                return BadRequest("Telegram ID не указан");

            var token = await _authService.GetJwtTokenAsync(telegramId.Value);

            if (string.IsNullOrEmpty(token))
                return RedirectToPage("/Error", new { message = "Ошибка авторизации" });

            Response.Cookies.Append("AuthToken", token, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });

            return RedirectToPage("/Index");
        }
    }
}

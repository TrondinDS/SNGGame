using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Login
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// Обрабатывает запросы по адресу /Login или /Login/{jwtToken}
        /// </summary>
        /// <param name="jwtToken">JWT токен, переданный в URL</param>
        public IActionResult OnGet(string? jwtToken)
        {
            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                // Токен не передан — просто показываем страницу логина
                return Page();
            }

            // Устанавливаем токен в куки
            Response.Cookies.Append("AuthToken", jwtToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });

            // Перенаправляем на главную
            return RedirectToPage("/Index");
        }
    }
}

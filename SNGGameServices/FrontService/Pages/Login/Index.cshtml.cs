using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Login
{
    public class IndexModel : PageModel
    {
        /// <summary>
        /// ������������ ������� �� ������ /Login ��� /Login/{jwtToken}
        /// </summary>
        /// <param name="jwtToken">JWT �����, ���������� � URL</param>
        public IActionResult OnGet(string? jwtToken)
        {
            if (string.IsNullOrWhiteSpace(jwtToken))
            {
                // ����� �� ������� � ������ ���������� �������� ������
                return Page();
            }

            // ������������� ����� � ����
            Response.Cookies.Append("AuthToken", jwtToken, new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(1)
            });

            // �������������� �� �������
            return RedirectToPage("/Index");
        }
    }
}

using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.User
{
    public class DeleteModel : PageModel
    {
        private readonly IUserApiService _userApiService;

        public DeleteModel(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        [BindProperty]
        public UserDTO? User { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var user = await _userApiService.GetUserByIdAsync(id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "������������ �� ������.";
                return RedirectToPage("Index");
            }

            User = user;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (User == null || User.Id == Guid.Empty)
            {
                TempData["ErrorMessage"] = "������������ ������������� ������������.";
                return RedirectToPage("Index");
            }

            var success = await _userApiService.DeleteUserAsync(User.Id);
            if (!success)
            {
                TempData["ErrorMessage"] = "������ ��� �������� ������������.";
                return RedirectToPage("Index");
            }

            return RedirectToPage("Index");
        }
    }
}

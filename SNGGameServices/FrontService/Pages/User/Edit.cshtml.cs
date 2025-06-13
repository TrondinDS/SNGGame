using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FrontService.Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace FrontService.Pages.User 
{
    public class EditModel : PageModel
    {
        private readonly IUserApiService _userApiService;

        public EditModel(IUserApiService userApiService)
        {
            _userApiService = userApiService;
        }

        public UserDTO User { get; set; }

        [BindProperty]
        public UserDTO UserNew { get; set; }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var user = await _userApiService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            User = user;
            UserNew = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Login = user.Login,
                DateBirth = user.DateBirth,
                Content = user.Content,
                Image = user.Image,
                ImageType = user.ImageType
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userApiService.GetUserByIdAsync(UserNew.Id);
            if (user == null)
            {
                TempData["ErrorMessage"] = "Пользователь не найден.";
                return RedirectToPage(); // перезагрузка вызовет показ модального окна
            }

            user.Name = UserNew.Name;
            user.Login = UserNew.Login;
            user.DateBirth = UserNew.DateBirth;
            user.Content = UserNew.Content;

            if (ImageFile is { Length: > 0 })
            {
                var allowedTypes = new[] { "image/jpeg", "image/png" };
                if (!allowedTypes.Contains(ImageFile.ContentType))
                {
                    TempData["ErrorMessage"] = "Разрешены только JPG и PNG файлы.";
                    return RedirectToPage();
                }

                try
                {
                    using var memoryStream = new MemoryStream();
                    await ImageFile.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    user.Image = Convert.ToBase64String(fileBytes);
                    user.ImageType = ImageFile.ContentType;
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Ошибка обработки изображения: {ex.Message}";
                    return RedirectToPage();
                }
            }

            var success = await _userApiService.UpdateUserAsync(user.Id, user);
            if (!success)
            {
                TempData["ErrorMessage"] = "Ошибка при сохранении данных.";
                return RedirectToPage();
            }

            return RedirectToPage("Index");
        }
    }
}

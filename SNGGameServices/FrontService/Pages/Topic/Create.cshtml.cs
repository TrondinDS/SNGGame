using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Topic
{
    public class CreateModel : PageModel
    {
        private readonly ITopicApiService _topicApiService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateModel(ITopicApiService topicApiService, IHttpContextAccessor httpContextAccessor)
        {
            _topicApiService = topicApiService;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public TopicCreateDTO NewTopic { get; set; }

        public IActionResult OnGet()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
            {
                TempData["AuthError"] = "Вы не авторизованы. Войдите в систему, чтобы создать тему.";
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
            {
                TempData["AuthError"] = "Вы не авторизованы. Войдите в систему, чтобы создать тему.";
                return RedirectToPage(); // Возврат на ту же страницу для показа ошибки
            }

            if (!ModelState.IsValid)
                return Page();

            var success = await _topicApiService.CreateTopicAsync(NewTopic);
            if (success == null)
            {
                TempData["ErrorMessage"] = "Не удалось создать тему.";
                return RedirectToPage();
            }

            TempData["SuccessMessage"] = "Тема успешно создана!";
            return RedirectToPage("Index");
        }

        private string? GetToken()
        {
            return _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
        }
    }
}

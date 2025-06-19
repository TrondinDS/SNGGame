using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using Library.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

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

            // ��������� ������ ����� ��������� �� enum
            EntityTypes = Enum.GetValues(typeof(EntityType.Type))
                              .Cast<EntityType.Type>()
                              .Select(e => new SelectListItem
                              {
                                  Value = ((int)e).ToString(),
                                  Text = e.ToString()
                              }).ToList();
        }

        [BindProperty]
        public TopicCreateDTO NewTopic { get; set; }

        public List<SelectListItem> EntityTypes { get; set; } = new();

        public IActionResult OnGet()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
            {
                TempData["AuthError"] = "�� �� ������������. ������� � �������, ����� ������� ����.";
                return Page();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var token = GetToken();
            if (string.IsNullOrEmpty(token))
            {
                TempData["AuthError"] = "�� �� ������������. ������� � �������, ����� ������� ����.";
                return RedirectToPage();
            }

            if (!ModelState.IsValid)
                return Page();

            var success = await _topicApiService.CreateTopicAsync(NewTopic);
            if (success == null)
            {
                TempData["ErrorMessage"] = "�� ������� ������� ����.";
                return RedirectToPage();
            }

            TempData["SuccessMessage"] = "���� ������� �������!";
            return RedirectToPage("Index");
        }

        private string? GetToken() =>
            _httpContextAccessor.HttpContext?.Request.Cookies["AuthToken"];
    }
}

using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Topic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Topic
{
    public class EditModel : PageModel
    {
        private readonly ITopicApiService _topicApiService;

        public EditModel(ITopicApiService topicApiService)
        {
            _topicApiService = topicApiService;
        }

        public TopicDTO Topic { get; set; }

        [BindProperty]
        public TopicDTO TopicNew { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var topic = await _topicApiService.GetTopicByIdAsync(id);
            if (topic == null)
                return NotFound();

            Topic = topic;

            // �������� � BindProperty ������
            TopicNew = new TopicDTO
            {
                Id = topic.Id,
                Title = topic.Title,
                Description = topic.Description,
                DateCreated = topic.DateCreated
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existing = await _topicApiService.GetTopicByIdAsync(TopicNew.Id);
            if (existing == null)
            {
                TempData["ErrorMessage"] = "���� �� �������.";
                return RedirectToPage();
            }

            // ���������� ������
            existing.Title = TopicNew.Title;
            existing.Description = TopicNew.Description;

            var success = await _topicApiService.UpdateTopicAsync(existing.Id, existing);
            if (!success)
            {
                TempData["ErrorMessage"] = "������ ��� ���������� ����.";
                return RedirectToPage();
            }

            return RedirectToPage("Index");
        }
    }
}

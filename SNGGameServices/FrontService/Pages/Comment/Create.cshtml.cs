using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;
using Library.Generics.DB.DTO.DTOModelView.UserActivityService.Topic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Comment
{
    public class CreateModel : PageModel
    {
        private readonly ICommentApiService _commentApiService;
        private readonly ITopicApiService _topicApiService;

        public CreateModel(ICommentApiService commentApiService, ITopicApiService topicApiService)
        {
            _commentApiService = commentApiService;
            _topicApiService = topicApiService;
        }

        // ������������ �������� TopicId ����� query (?topicId=...)
        [BindProperty(SupportsGet = true)]
        public Guid TopicId { get; set; }

        [BindProperty]
        public CommentCreateDTO NewComment { get; set; }

        // ����� � ������������� ��� �����������
        public TopicDTOView? TopicWithComments { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (TopicId == Guid.Empty)
                return NotFound();

            // ��������� ����� � �����������
            var topic = await _topicApiService.GetTopicByIdAsync(TopicId);

            if (topic == null)
                return NotFound();

            var topics = await _topicApiService.GetTopicsByEntityIdAsync(new List<Guid> { topic.EntityId });
            TopicWithComments = topics?.FirstOrDefault(u => u.Topic.Id == topic.Id);

            if (TopicWithComments == null)
                return NotFound();

            NewComment = new CommentCreateDTO { Body = "" };
            NewComment.TopicId = TopicId;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            // ��������� �����
            var token = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrWhiteSpace(token))
            {
                TempData["AuthError"] = "����������, ������� � �������, ����� �������� �����������.";
                return RedirectToPage(new { TopicId });
            }

            if (!ModelState.IsValid)
            {
                // ��� ������ ������ ������ ��������� ����� � ����������� ��� ������
                await OnGetAsync();
                return Page();
            }

            NewComment.CountLike = 0;

            var success = await _commentApiService.CreateCommentAsync(NewComment);
            if (success == null)
            {
                TempData["ErrorMessage"] = "������ ��� ���������� �����������.";
                return RedirectToPage(new { TopicId });
            }

            // ������������� ��������, ����� �������� �����������
            return RedirectToPage(new { TopicId });
        }
    }
}

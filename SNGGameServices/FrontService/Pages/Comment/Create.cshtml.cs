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

        // Поддерживаем передачу TopicId через query (?topicId=...)
        [BindProperty(SupportsGet = true)]
        public Guid TopicId { get; set; }

        [BindProperty]
        public CommentCreateDTO NewComment { get; set; }

        // Топик с комментариями для отображения
        public TopicDTOView? TopicWithComments { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (TopicId == Guid.Empty)
                return NotFound();

            // Загружаем топик и комментарии
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
            // Проверяем токен
            var token = HttpContext.Request.Cookies["AuthToken"];
            if (string.IsNullOrWhiteSpace(token))
            {
                TempData["AuthError"] = "Пожалуйста, войдите в систему, чтобы оставить комментарий.";
                return RedirectToPage(new { TopicId });
            }

            if (!ModelState.IsValid)
            {
                // При ошибке модели заново загружаем топик и комментарии для показа
                await OnGetAsync();
                return Page();
            }

            NewComment.CountLike = 0;

            var success = await _commentApiService.CreateCommentAsync(NewComment);
            if (success == null)
            {
                TempData["ErrorMessage"] = "Ошибка при добавлении комментария.";
                return RedirectToPage(new { TopicId });
            }

            // Перезагружаем страницу, чтобы обновить комментарии
            return RedirectToPage(new { TopicId });
        }
    }
}

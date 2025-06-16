using Library.Generics.DB.DTO.DTOModelView.StudioGameService.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text;
using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelView.UserActivityService.Topic;

namespace FrontService.Pages.Game
{
    public class IDModel : PageModel
    {
        private readonly IGameApiService _gameApiService;
        private readonly ITopicApiService _topicApiService;

        public IDModel(IGameApiService gameApiService, ITopicApiService topicApiService)
        {
            _gameApiService = gameApiService;
            _topicApiService = topicApiService;
        }

        [BindProperty(SupportsGet = true)]
        public Guid Id { get; set; }

        public GameDTOView? GameView { get; set; }

        // Список тем, связанных с данной игрой
        public List<TopicDTOView> Topics { get; set; } = new();

        public async Task<IActionResult> OnGetAsync()
        {
            if (Id == Guid.Empty)
                return NotFound("Некорректный идентификатор игры");

            var games = await _gameApiService.GetGameDTOViewByIdGamesAsync(new List<Guid> { Id });
            GameView = games?.FirstOrDefault();

            if (GameView == null)
                return NotFound("Игра не найдена");

            // Получение тем, связанных с этой игрой
            var topicsResult = await _topicApiService.GetTopicsByEntityIdAsync(new List<Guid> { Id });
            if (topicsResult != null)
                Topics = topicsResult.ToList();

            return Page();
        }
    }
}

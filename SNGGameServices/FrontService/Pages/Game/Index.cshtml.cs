using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelView.StudioGameService.Game;
using Library.Generics.Query.QueryModels.StudioGame;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Game
{
    public class IndexModel : PageModel
    {
        private readonly IGameApiService _gameApiService;

        public IndexModel(IGameApiService gameApiService)
        {
            _gameApiService = gameApiService;
        }

        public List<GameDTO> Games { get; set; } = new();

        public async Task OnGetAsync()
        {
            var param = new ParamQueryGame();
            var result = await _gameApiService.GetAllGamesAsync();
            Games = result?.ToList() ?? new List<GameDTO>();
        }
    }
}

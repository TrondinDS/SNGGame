using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Genre;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Tag;
using Library.Generics.DB.DTO.DTOModelView.StudioGameService.Game;
using Library.Generics.Query.QueryModels.StudioGame;
using Library.Generics.Query.QueryModels.StudioGame.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Game
{
    public class IndexModel : PageModel
    {
        private readonly IGameApiService _gameApiService;
        private readonly IGenreApiService _genreApiService;
        private readonly ITagApiService _tagApiService;

        public IndexModel(IGameApiService gameApiService, IGenreApiService genreApiService, ITagApiService tagApiService)
        {
            _gameApiService = gameApiService;
            _genreApiService = genreApiService;
            _tagApiService = tagApiService;
        }

        public List<GameDTO> Games { get; set; } = new();
        public List<TagDTO> Tags { get; set; } = new();
        public List<GenreDTO> Genres { get; set; } = new();

        // Фильтры
        [BindProperty(SupportsGet = true)]
        public string? Title { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<Guid> SelectedGenres { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public List<Guid> SelectedTags { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public int? Rating { get; set; }

        [BindProperty(SupportsGet = true)]
        public bool Top { get; set; }

        public async Task OnGetAsync()
        {
            // Загружаем все теги и жанры для фильтра (независимо от фильтрации)
            Tags = (await _tagApiService.GetAllTagsAsync())?.ToList() ?? new();
            Genres = (await _genreApiService.GetAllGenresAsync())?.ToList() ?? new();

            var hasFilter =
                !string.IsNullOrWhiteSpace(Title) ||
                SelectedGenres.Any() ||
                SelectedTags.Any() ||
                Rating.HasValue ||
                Top;

            if (hasFilter)
            {
                // С фильтрацией
                var filter = new ParamQueryGame
                {
                    QueryGame = new()
                    {
                        TitleGame = Title
                    },
                    QueryGenre = new()
                    {
                        ListGenreId = SelectedGenres
                    },
                    QueryTag = new()
                    {
                        ListTagId = SelectedTags
                    },
                    QueryLibrary = new()
                    {
                        Rating = Rating
                    },
                    QueryTop = new()
                    {
                        Top = Top
                    }
                };

                Games = (await _gameApiService.GetFilteredGamesAsync(filter))?.ToList() ?? new();
            }
            else
            {
                // Без фильтрации — все игры
                Games = (await _gameApiService.GetAllGamesAsync())?.ToList() ?? new();
            }
        }
    }
}

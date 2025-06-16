using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Game
{
    public class EditModel : PageModel
    {
        private readonly IGameApiService _gameApiService;

        public EditModel(IGameApiService gameApiService)
        {
            _gameApiService = gameApiService;
        }

        public GameDTO Game { get; set; }

        [BindProperty]
        public GameDTO GameNew { get; set; }

        [BindProperty]
        public IFormFile? ImageFile { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid id)
        {
            var game = await _gameApiService.GetGameByIdAsync(id);
            if (game == null)
                return NotFound();

            Game = game;

            GameNew = new GameDTO
            {
                Id = game.Id,
                RussianTitle = game.RussianTitle,
                EnglishTitle = game.EnglishTitle,
                AlternativeTitle = game.AlternativeTitle,
                ShortDescription = game.ShortDescription,
                LinkSite = game.LinkSite,
                Publisher = game.Publisher,
                ReleaseDate = game.ReleaseDate,
                CountryDevelopment = game.CountryDevelopment,
                LinkPageStore = game.LinkPageStore,
                Platform = game.Platform,
                StatisticGameId = game.StatisticGameId,
                StudioId = game.StudioId,
                Image = game.Image,
                ImageType = game.ImageType,
                Content = game.Content,
                IsDeleted = game.IsDeleted,
                DateDeleted = game.DateDeleted
            };

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var existing = await _gameApiService.GetGameByIdAsync(GameNew.Id);
            if (existing == null)
            {
                TempData["ErrorMessage"] = "Игра не найдена.";
                return RedirectToPage();
            }

            // Обновление данных
            existing.RussianTitle = GameNew.RussianTitle;
            existing.EnglishTitle = GameNew.EnglishTitle;
            existing.AlternativeTitle = GameNew.AlternativeTitle;
            existing.ShortDescription = GameNew.ShortDescription;
            existing.LinkSite = GameNew.LinkSite;
            existing.Publisher = GameNew.Publisher;
            existing.ReleaseDate = GameNew.ReleaseDate;
            existing.CountryDevelopment = GameNew.CountryDevelopment;
            existing.LinkPageStore = GameNew.LinkPageStore;
            existing.Platform = GameNew.Platform;
            //existing.StudioId = GameNew.StudioId;
            //existing.StatisticGameId = GameNew.StatisticGameId;
            existing.Content = GameNew.Content;
            //existing.IsDeleted = GameNew.IsDeleted;
            //existing.DateDeleted = GameNew.DateDeleted;

            // Обработка изображения
            if (ImageFile is { Length: > 0 })
            {
                var allowedTypes = new[] { "image/jpeg", "image/png" };
                if (!allowedTypes.Contains(ImageFile.ContentType))
                {
                    TempData["ErrorMessage"] = "Разрешены только изображения JPG и PNG.";
                    return RedirectToPage();
                }

                try
                {
                    using var memoryStream = new MemoryStream();
                    await ImageFile.CopyToAsync(memoryStream);
                    var fileBytes = memoryStream.ToArray();

                    existing.Image = Convert.ToBase64String(fileBytes);
                    existing.ImageType = ImageFile.ContentType;
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = $"Ошибка обработки изображения: {ex.Message}";
                    return RedirectToPage();
                }
            }

            var success = await _gameApiService.UpdateGameAsync(existing);
            if (!success)
            {
                TempData["ErrorMessage"] = "Ошибка при сохранении изменений.";
                return RedirectToPage();
            }

            return RedirectToPage("Index");
        }
    }
}

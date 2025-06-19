using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.UserService.Banned;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Ban
{
    public class IndexModel : PageModel
    {
        private readonly IBannedApiService _banApiService;

        public IndexModel(IBannedApiService banApiService)
        {
            _banApiService = banApiService;
        }

        public List<BannedDTO> Bans { get; private set; } = new();

        [TempData]
        public string? ErrorMessage { get; set; }

        public async Task OnGetAsync()
        {
            try
            {
                var bans = await _banApiService.GetAllBannedAsync(); // должен быть метод получения всех банов
                Bans = bans?.ToList() ?? new List<BannedDTO>();
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Ошибка при получении банов: {ex.Message}";
                Bans = new();
            }
        }
    }
}

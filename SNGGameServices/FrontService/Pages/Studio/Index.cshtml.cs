using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Studio
{
    public class IndexModel : PageModel
    {
        private readonly IStudioApiService _studioApiService;

        public IndexModel(IStudioApiService studioApiService)
        {
            _studioApiService = studioApiService;
        }

        public List<StudioDTO> Studios { get; set; } = new();

        public async Task OnGetAsync()
        {
            var result = await _studioApiService.GetAllStudiosAsync();
            Studios = result?.ToList() ?? new();
        }
    }
}

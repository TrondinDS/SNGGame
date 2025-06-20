using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Organizer
{
    public class IndexModel : PageModel
    {
        private readonly IOrganizerService service;

        public IndexModel(IOrganizerService service)
        {
            this.service = service;
        }

        public List<OrganizerDTO> Organizers { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Title { get; set; }

        public async Task OnGetAsync()
        {
            var res = await service.GetAll();
            Organizers = res?.ToList() ?? new();
        }
    }
}

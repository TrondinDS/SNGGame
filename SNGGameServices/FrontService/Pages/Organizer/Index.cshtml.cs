using DnsClient;
using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Library.Generics.Query.QueryModels.OrganizerEvent;

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

        [BindProperty(SupportsGet = true)]
        public string? Mail { get; set; }

        public async Task OnGetAsync()
        {
            var hasFilter =
                !string.IsNullOrWhiteSpace(Title) ||
                !string.IsNullOrWhiteSpace(Mail);
            if (hasFilter)
            {
                Organizers = (await service.Filter(
                    new ParamQueryOrganizer
                    {
                        QueryOrganizer = new()
                        {
                            Title = Title,
                            Mail = Mail,
                        },
                    }
                    )
                 )?.ToList() ?? new();
                return;
            }
            var res = await service.GetAll();
            Organizers = res?.ToList() ?? new();
        }
    }
}

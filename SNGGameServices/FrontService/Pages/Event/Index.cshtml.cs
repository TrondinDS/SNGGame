using FrontService.Services.Interfaces;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using Library.Generics.Query.QueryModels.OrganizerEvent.Event;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FrontService.Pages.Event
{
    public class IndexModel : PageModel
    {
        private readonly IEventApiService service;

        public IndexModel(IEventApiService eventService)
        {
            service = eventService;
        }

        public List<EventDTO> Events { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Title { get; set; }


        [BindProperty(SupportsGet = true)]
        public string? Region { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? City { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Country { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Address { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Status { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? PriceMin { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? PriceMax { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? GeoUrl { get; set; }


        public async Task OnGetAsync()
        {
            Events = (await service.FilterEventsAsync(
                new ParamQueryEvent
                {
                    QueryEvent = new QueryEvent
                    {
                        Title = Title,
                        Region = Region,
                        City = City,
                        Country = Country,
                        Address = Address,
                        Status = Status,
                        PriceMin = PriceMin,
                        PriceMax = PriceMax,
                        GeoUrl = GeoUrl,
                    }
                }
            ))?.ToList() ?? new();
        }
    }
}

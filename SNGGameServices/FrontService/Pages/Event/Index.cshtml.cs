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
        private readonly IEventApiService _eventService;

        public IndexModel(IEventApiService eventService)
        {
            _eventService = eventService;
        }

        public List<EventDTO> Events { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Title { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? City { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Country { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Region { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? Status { get; set; }

        public async Task OnGetAsync()
        {
            var query = new ParamQueryEvent
            {
                QueryEvent = new QueryEvent
                {
                    Title = Title,
                    City = City,
                    Country = Country,
                    Region = Region,
                    Status = Status
                }
            };

            var result = await _eventService.FilterEventsAsync(query);
            Events = result?.ToList() ?? new();
        }
    }
}

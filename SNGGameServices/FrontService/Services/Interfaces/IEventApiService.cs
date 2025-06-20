using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using Library.Generics.Query.QueryModels.OrganizerEvent;

namespace FrontService.Services.Interfaces
{
    public interface IEventApiService
    {
        Task<IEnumerable<EventDTO>?> GetAllEventsAsync();
        Task<EventDTO?> GetEventByIdAsync(Guid id);
        Task<EventDTO?> CreateEventAsync(EventDTO dto);
        Task<bool> UpdateEventAsync(EventDTO dto);
        Task<bool> DeleteEventAsync(Guid id);
        Task<IEnumerable<EventDTO>?> FilterEventsAsync(ParamQueryEvent query);
    }
}

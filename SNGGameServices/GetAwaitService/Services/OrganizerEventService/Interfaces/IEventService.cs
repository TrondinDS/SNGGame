using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using Library.Generics.Query.QueryModels.OrganizerEvent;

namespace GetAwaitService.Services.OrganizerEventService.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>?> GetAll();
        Task<IEnumerable<EventDTO>?> Filter(ParamQueryEvent param);

        Task<EventDTO?> GetById(Guid id);
        Task<EventDTO?> Create(EventDTO userDto);
        Task<bool> Update(EventDTO userDto);
        Task<bool> Delete(Guid id);
    }
}

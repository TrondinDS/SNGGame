using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;

namespace GetAwaitService.Services.OrganizerEventService.Interfaces
{
    public interface IEventService
    {
        Task<IEnumerable<EventDTO>?> GetAll();
        Task<EventDTO?> GetById(Guid id);
        Task<EventDTO?> Create(EventDTO userDto);
        Task<bool> Update(EventDTO userDto);
        Task<bool> Delete(Guid id);
    }
}

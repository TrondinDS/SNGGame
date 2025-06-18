using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using Library.Generics.Query.QueryModels.OrganizerEvent;

namespace OrganizerEventService.Services.Interfaces
{
    public interface IEventService
    {
        Task AddAsync(EventDTO dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<EventDTO>> GetAllAsync();
        Task<EventDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(EventDTO dto);
        Task<IEnumerable<EventDTO>> Filter(ParamQueryEvent param);
    }
}

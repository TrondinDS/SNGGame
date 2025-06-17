using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;

namespace GetAwaitService.Services.OrganizerEventService.Interfaces
{
    public interface IOrganizerService
    {
        Task<IEnumerable<OrganizerDTO>?> GetAll();
        Task<OrganizerDTO?> GetById(Guid id);
        Task<OrganizerDTO?> Create(OrganizerDTO userDto);
        Task<bool> Update(OrganizerDTO userDto);
        Task<bool> Delete(Guid id);
    }
}

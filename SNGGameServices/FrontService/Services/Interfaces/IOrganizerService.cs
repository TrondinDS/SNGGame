using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;

namespace FrontService.Services.Interfaces;

public interface IOrganizerService
{
    Task<IEnumerable<OrganizerDTO>?> GetAll();
    Task<OrganizerDTO?> GetById(Guid id);
    Task<OrganizerDTO?> Create(OrganizerDTO userDto);
    Task<bool> Update(OrganizerDTO userDto);
    Task<bool> Delete(Guid id);
    Task<IEnumerable<OrganizerDTO>?> GetByUserId(Guid id);
}

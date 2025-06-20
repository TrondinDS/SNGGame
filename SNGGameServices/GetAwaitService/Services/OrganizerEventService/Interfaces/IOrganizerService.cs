using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Library.Generics.Query.QueryModels.OrganizerEvent;
using Microsoft.AspNetCore.Mvc;

namespace GetAwaitService.Services.OrganizerEventService.Interfaces;

public interface IOrganizerService
{
    Task<IEnumerable<OrganizerDTO>?> GetAll();
    Task<OrganizerDTO?> GetById(Guid id);
    Task<OrganizerDTO?> Create(OrganizerDTO userDto);
    Task<bool> Update(OrganizerDTO userDto);
    Task<bool> Delete(Guid id);
    Task<IEnumerable<OrganizerDTO>?> GetByUserId(Guid id);
}

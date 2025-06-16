using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ComplainTicket;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;

namespace GetAwaitService.Services.ChatFeedbackService.Interfaces
{
    public interface IComplainTicketService
    {
        Task<IEnumerable<ComplainTicketDTO>?> GetAll();
        Task<ComplainTicketDTO?> GetById(Guid id);
        Task<ComplainTicketDTO?> Create(ComplainTicketDTO dto);
        Task<bool> Update(ComplainTicketDTO dto);
        Task<bool> Delete(Guid id);
    }
}

using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;

namespace GetAwaitService.Services.ChatFeedbackService.Interfaces
{
    public interface IChatFeedbackService
    {
        Task<IEnumerable<ChatFeedbackDTO>?> GetAll();
        Task<ChatFeedbackDTO?> GetById(Guid id);
        Task<ChatFeedbackDTO?> Create(ChatFeedbackDTO userDto);
        Task<bool> Update(ChatFeedbackDTO userDto);
        Task<bool> Delete(Guid id);
    }
}

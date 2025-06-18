using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;
using Library.Generics.Query.QueryModels.Administratum;

namespace AdministratumService.Services.Interfaces
{
    public interface IChatFeedbackService
    {
        Task AddAsync(ChatFeedbackDTO dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ChatFeedbackDTO>> GetAllAsync();
        Task<ChatFeedbackDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(ChatFeedbackDTO dto);
        Task<IEnumerable<ChatFeedbackDTO>?> Filter(ParamQueryChatfeedback param);

    }
}

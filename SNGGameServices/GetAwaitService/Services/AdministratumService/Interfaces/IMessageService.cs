using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ComplainTicket;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.Message;
using Library.Generics.Query.QueryModels.Administratum;

namespace GetAwaitService.Services.ChatFeedbackService.Interfaces
{
    public interface IMessageService
    {
        Task<IEnumerable<MessageDTO>?> GetAll();
        Task<MessageDTO?> GetById(Guid id);
        Task<MessageDTO?> Create(MessageDTO dto);
        Task<bool> Update(MessageDTO dto);
        Task<bool> Delete(Guid id);
        Task<IEnumerable<MessageDTO>?> Filter(ParamQueryMessage param);
    }
}

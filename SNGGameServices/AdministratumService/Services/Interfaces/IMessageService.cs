using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.Message;
using Library.Generics.Query.QueryModels.Administratum;

namespace AdministratumService.Services.Interfaces
{
    public interface IMessageService
    {
        Task AddAsync(MessageDTO dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<MessageDTO>> GetAllAsync();
        Task<MessageDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(MessageDTO dto);
        Task<IEnumerable<MessageDTO>> Filter(ParamQueryMessage param);
    }
}

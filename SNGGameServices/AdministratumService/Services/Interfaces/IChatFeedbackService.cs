using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;

namespace AdministratumService.Services.Interfaces
{
    public interface IChatFeedbackService
    {
        Task AddAsync(ChatFeedbackDTO dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ChatFeedbackDTO>> GetAllAsync();
        Task<ChatFeedbackDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(ChatFeedbackDTO dto);

        //Task<IEnumerable<GameDTO>> FilterGame(ParamQueryGame paramQuerySG);

        //public Task<IEnumerable<Game>> GetAllCardGameAsync();
        //public Task<IEnumerable<Game>> GetSelectCardGameAsync(IEnumerable<Guid> idGames);
        //public Task<IEnumerable<Game>> GetFiltreCardGameAsync(ParamQueryGame paramQuerySG);

        //public Task<IEnumerable<StatisticGame>> GetStatisticGames(IEnumerable<Guid> listGameId);
    }
}

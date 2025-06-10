using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ComplainTicket;

namespace AdministratumService.Services.Interfaces
{
    public interface IComplainTicketService
    {
        Task AddAsync(ComplainTicketDTO dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<ComplainTicketDTO>> GetAllAsync();
        Task<ComplainTicketDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(ComplainTicketDTO dto);

        //Task<IEnumerable<GameDTO>> FilterGame(ParamQueryGame paramQuerySG);

        //public Task<IEnumerable<Game>> GetAllCardGameAsync();
        //public Task<IEnumerable<Game>> GetSelectCardGameAsync(IEnumerable<Guid> idGames);
        //public Task<IEnumerable<Game>> GetFiltreCardGameAsync(ParamQueryGame paramQuerySG);

        //public Task<IEnumerable<StatisticGame>> GetStatisticGames(IEnumerable<Guid> listGameId);
    }
}

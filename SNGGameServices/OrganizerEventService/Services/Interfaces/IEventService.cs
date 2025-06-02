using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Event;

namespace OrganizerEventService.Services.Interfaces
{
    public interface IEventService
    {
        Task AddAsync(EventDTO dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<EventDTO>> GetAllAsync();
        Task<EventDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(EventDTO dto);

        //Task<IEnumerable<GameDTO>> FilterGame(ParamQueryGame paramQuerySG);

        //public Task<IEnumerable<Game>> GetAllCardGameAsync();
        //public Task<IEnumerable<Game>> GetSelectCardGameAsync(IEnumerable<Guid> idGames);
        //public Task<IEnumerable<Game>> GetFiltreCardGameAsync(ParamQueryGame paramQuerySG);

        //public Task<IEnumerable<StatisticGame>> GetStatisticGames(IEnumerable<Guid> listGameId);
    }
}

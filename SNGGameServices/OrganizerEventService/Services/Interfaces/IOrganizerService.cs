using Library.Generics.DB.DTO.DTOModelServices.OrganizerEventService.Organizer;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.Query.QueryModels.StudioGame;

namespace OrganizerEventService.Services.Interfaces
{
    public interface IOrganizerService
    {
        Task AddAsync(OrganizerDTO dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<OrganizerDTO>> GetAllAsync();
        Task<GameDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(OrganizerDTO dto);

        //Task<IEnumerable<GameDTO>> FilterGame(ParamQueryGame paramQuerySG);

        //public Task<IEnumerable<Game>> GetAllCardGameAsync();
        //public Task<IEnumerable<Game>> GetSelectCardGameAsync(IEnumerable<Guid> idGames);
        //public Task<IEnumerable<Game>> GetFiltreCardGameAsync(ParamQueryGame paramQuerySG);

        //public Task<IEnumerable<StatisticGame>> GetStatisticGames(IEnumerable<Guid> listGameId);
    }
}

using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.ChatFeedback;
using Library.Generics.DB.DTO.DTOModelServices.AdministratumService.Message;

namespace AdministratumService.Services.Interfaces
{
    public interface IMessageService
    {
        Task AddAsync(MessageDTO dto);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<MessageDTO>> GetAllAsync();
        Task<MessageDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(MessageDTO dto);

        //Task<IEnumerable<GameDTO>> FilterGame(ParamQueryGame paramQuerySG);

        //public Task<IEnumerable<Game>> GetAllCardGameAsync();
        //public Task<IEnumerable<Game>> GetSelectCardGameAsync(IEnumerable<Guid> idGames);
        //public Task<IEnumerable<Game>> GetFiltreCardGameAsync(ParamQueryGame paramQuerySG);

        //public Task<IEnumerable<StatisticGame>> GetStatisticGames(IEnumerable<Guid> listGameId);
    }
}

using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.StatisticGame;
using Library.Generics.DB.DTO.DTOModelView.StudioGameService.Game;
using Library.Generics.Query.QueryModels.StudioGame;

namespace GetAwaitService.Services.StudioGameService.Interfaces
{
    public interface IGameService
    {
        Task<IEnumerable<GameDTO>?> GetAllAsync();
        Task<GameDTO?> GetByIdAsync(Guid id);
        Task<GameDTO?> CreateAsync(GameDTO dto);
        Task<bool> UpdateAsync(Guid id, GameDTO dto);
        Task<bool> DeleteAsync(Guid id);
        Task<IEnumerable<GameDTO>?> GetFilteredAsync(ParamQueryGame query);
        Task<IEnumerable<StatisticGameDTO>?> GetStatisticsAsync(List<Guid> gameIds);
        Task<IEnumerable<GameDTOView>?> GetGameDTOViewByIdGamesAsync(List<Guid> gameIds);
    }
}

using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.StatisticGame;
using Library.Generics.DB.DTO.DTOModelView.StudioGameService.Game;
using Library.Generics.Query.QueryModels.StudioGame;

namespace FrontService.Services.Interfaces
{
    public interface IGameApiService
    {
        Task<IEnumerable<GameDTO>?> GetAllGamesAsync();
        Task<GameDTO?> GetGameByIdAsync(Guid id);
        Task<GameDTO?> CreateGameAsync(GameCreateDTO dto);
        Task<bool> UpdateGameAsync(GameDTO dto);
        Task<bool> DeleteGameAsync(Guid id);
        Task<IEnumerable<GameDTO>?> GetFilteredGamesAsync(ParamQueryGame query);
        Task<IEnumerable<GameDTOView>?> GetGameDTOViewByIdGamesAsync(List<Guid> gameIds);
    }
}

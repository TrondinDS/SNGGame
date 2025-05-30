using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Studio;
using Library.Generics.Query.QueryModels.StudioGame;
using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IStudioService
    {
        Task AddAsync(StudioDTO studio);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<StudioDTO>> GetAllAsync();
        Task<StudioDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(StudioDTO studio);
        public Task<IEnumerable<StudioDTO>> GetFiltreCardStudioAsync(ParamQueryStudio paramQueryListStudio);
        public Task<IEnumerable<Studio>> GetStudioByUserIdAsync(Guid id);
    }
}

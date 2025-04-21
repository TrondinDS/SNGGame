using Library.Generics.Query.QueryModels.StudioGame;
using Library.Generics.Query.QueryModels.StudioGame.Studio;
using StudioGameService.DB.Model;

namespace StudioGameService.Services.Interfaces
{
    public interface IStudioService
    {
        Task AddAsync(Studio studio);
        Task DeleteAsync(int id);
        Task<IEnumerable<Studio>> GetAllAsync();
        Task<Studio> GetByIdAsync(int id);
        Task UpdateAsync(Studio studio);
        public Task<IEnumerable<Studio>> GetFiltreCardStudioAsync(QueryListStudio paramQueryListStudio);
    }
}

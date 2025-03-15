using StudioGameService.DB.Model;
using StudioGameService.Repository;
using StudioGameService.Repository.Interfaces;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Services
{
    public class StudioService : IStudioService
    {
        private readonly IStudioRepository studioRepository;

        public StudioService(IStudioRepository studioRepository)
        {
            this.studioRepository = studioRepository;
        }

        public async Task AddAsync(Studio studio)
        {
            await studioRepository.AddAsync(studio);
            await studioRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var studio = await studioRepository.GetByIdAsync(id);
            if (studio != null)
            {
                studioRepository.DeleteAsync(studio);
                await studioRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Studio>> GetAllAsync()
        {
            return studioRepository.GetAllAsync();
        }

        public async Task<Studio> GetByIdAsync(int id)
        {
            return await studioRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Studio studio)
        {
            studioRepository.UpdateAsync(studio);
            await studioRepository.SaveChangesAsync();
        }
    }
}

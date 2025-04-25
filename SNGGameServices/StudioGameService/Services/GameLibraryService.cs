using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Services
{
    public class GameLibraryService : IGameLibraryService
    {
        protected readonly IGameLibraryRepository gameLibraryRepository;

        public GameLibraryService(IGameLibraryRepository gameLibraryRepository)
        {
            this.gameLibraryRepository = gameLibraryRepository;
        }

        public async Task AddAsync(GameLibrary gameLibrary)
        {
            await gameLibraryRepository.AddAsync(gameLibrary);
            await gameLibraryRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var gameLibrary = await gameLibraryRepository.GetByIdAsync(id);
            if (gameLibrary != null)
            {
                await gameLibraryRepository.DeleteAsync(gameLibrary);
                await gameLibraryRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<GameLibrary>> GetAllAsync()
        {
            return gameLibraryRepository.GetAllAsync();
        }

        public async Task<GameLibrary> GetByIdAsync(int id)
        {
            return await gameLibraryRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(GameLibrary gameLibrary)
        {
            await gameLibraryRepository.UpdateAsync(gameLibrary);
            await gameLibraryRepository.SaveChangesAsync();
        }
    }
}

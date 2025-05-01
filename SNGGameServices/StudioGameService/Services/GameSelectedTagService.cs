using StudioGameService.DB.Model;
using StudioGameService.Repository;
using StudioGameService.Repository.Interfaces;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Services
{
    public class GameSelectedTagService : IGameSelectedTagService
    {
        protected readonly IGameSelectedTagRepository gameSelectedTagRepository;

        public GameSelectedTagService(IGameSelectedTagRepository gameSelectedTagRepository)
        {
            this.gameSelectedTagRepository = gameSelectedTagRepository;
        }

        public async Task AddAsync(GameSelectedTag gameSelectedTag)
        {
            await gameSelectedTagRepository.AddAsync(gameSelectedTag);
            await gameSelectedTagRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var gameSelectedTag = await gameSelectedTagRepository.GetByIdAsync(id);
            if (gameSelectedTag != null)
            {
                gameSelectedTagRepository.DeleteAsync(gameSelectedTag);
                await gameSelectedTagRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<GameSelectedTag>> GetAllAsync()
        {
            return gameSelectedTagRepository.GetAllAsync();
        }

        public async Task<GameSelectedTag> GetByIdAsync(Guid id)
        {
            return await gameSelectedTagRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(GameSelectedTag gameSelectedTag)
        {
            gameSelectedTagRepository.UpdateAsync(gameSelectedTag);
            await gameSelectedTagRepository.SaveChangesAsync();
        }
    }
}

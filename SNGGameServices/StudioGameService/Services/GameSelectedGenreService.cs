using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;
using StudioGameService.Services.Interfaces;

namespace StudioGameService.Services
{
    public class GameSelectedGenreService : IGameSelectedGenreService
    {
        protected readonly IGameSelectedGenreRepository gameSelectedGenreRepository;

        public GameSelectedGenreService(IGameSelectedGenreRepository gameSelectedGenreRepository)
        {
            this.gameSelectedGenreRepository = gameSelectedGenreRepository;
        }

        public async Task AddAsync(GameSelectedGenre gameSelectedGenre)
        {
            await gameSelectedGenreRepository.AddAsync(gameSelectedGenre);
            await gameSelectedGenreRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var gameSelectedGenre = await gameSelectedGenreRepository.GetByIdAsync(id);
            if (gameSelectedGenre != null)
            {
                gameSelectedGenreRepository.DeleteAsync(gameSelectedGenre);
                await gameSelectedGenreRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<GameSelectedGenre>> GetAllAsync()
        {
            return gameSelectedGenreRepository.GetAllAsync();
        }

        public async Task<GameSelectedGenre> GetByIdAsync(int id)
        {
            return await gameSelectedGenreRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(GameSelectedGenre gameSelectedGenre)
        {
            gameSelectedGenreRepository.UpdateAsync(gameSelectedGenre);
            await gameSelectedGenreRepository.SaveChangesAsync();
        }
    }
}

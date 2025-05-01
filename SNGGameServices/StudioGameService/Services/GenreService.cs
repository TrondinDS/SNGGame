using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;
using StudioGameService.Services.Interfaces;

namespace StudioGenreService.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this.genreRepository = genreRepository;
        }

        public async Task AddAsync(Genre genre)
        {
            await genreRepository.AddAsync(genre);
            await genreRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var genre = await genreRepository.GetByIdAsync(id);
            if (genre != null)
            {
                genreRepository.DeleteAsync(genre);
                await genreRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Genre>> GetAllAsync()
        {
            return genreRepository.GetAllAsync();
        }

        public async Task<Genre> GetByIdAsync(Guid id)
        {
            return await genreRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Genre genre)
        {
            genreRepository.UpdateAsync(genre);
            await genreRepository.SaveChangesAsync();
        }
    }
}

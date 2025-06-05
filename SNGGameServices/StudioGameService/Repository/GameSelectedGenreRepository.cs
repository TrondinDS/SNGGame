using Library.Generics.GenericRepository;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;

namespace StudioGameService.Repository
{
    public class GameSelectedGenreRepository
        : GenericRepository<GameSelectedGenre, Guid>,
            IGameSelectedGenreRepository
    {
        public GameSelectedGenreRepository(ApplicationContext context)
            : base(context) { }
    }
}

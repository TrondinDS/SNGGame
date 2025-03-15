using Library.GenericRepository;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;

namespace StudioGameService.Repository
{
    public class GameLibraryRepository : GenericRepository<GameLibrary, int>, IGameLibraryRepository
    {
        public GameLibraryRepository(ApplicationContext context) : base(context)
        {
        }
    }
}

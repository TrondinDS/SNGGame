using Library.GenericRepository;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;

namespace StudioGameService.Repository
{
    public class GameRepository : GenericRepository<Game, int>, IGameRepository
    {
        public GameRepository(ApplicationContext context)
            : base(context) { }
    }
}

using Library.Generics.GenericRepository;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;

namespace StudioGameService.Repository
{
    public class GameSelectedTagRepository
        : GenericRepository<GameSelectedTag, int>,
            IGameSelectedTagRepository
    {
        public GameSelectedTagRepository(ApplicationContext context)
            : base(context) { }
    }
}

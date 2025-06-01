using Library;
using Library.Generics.GenericRepository;
using Library.Types;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;

namespace StudioGameService.Repository
{
    public class StatisticGameRepository : GenericRepository<StatisticGame, Guid>, IStatisticGameRepository
    {

        public StatisticGameRepository(ApplicationContext context)
            : base(context) {}

        
    }
}

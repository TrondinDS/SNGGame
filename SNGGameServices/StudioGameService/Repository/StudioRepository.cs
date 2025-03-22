using Library.Generics.GenericRepository;
using Microsoft.EntityFrameworkCore;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;

namespace StudioGameService.Repository
{
    public class StudioRepository : GenericRepository<Studio, int>, IStudioRepository
    {
        public StudioRepository(ApplicationContext context)
            : base(context) { }
    }
}

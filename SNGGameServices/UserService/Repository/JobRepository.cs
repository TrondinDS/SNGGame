using Library.Generics.GenericRepository;
using StudioGameService.DB.Model;
using UserService.DB.Context;
using UserService.Repository.Interfaces;

namespace UserService.Repository
{
    public class JobRepository : GenericRepository<Job, int>, IJobRepository
    {
        public JobRepository(ApplicationContext context)
            : base(context) { }
    }
}

using BannedService.DB.Models;
using Library.GenericRepository;
using UserService.DB.Context;
using UserService.Repository.Interfaces;

namespace UserService.Repository
{
    public class BannedRepository : GenericRepository<Banned, int>, IBannedRepository
    {
        public BannedRepository(ApplicationContext context) : base(context)
        { 
        }
    }
}

using Library.GenericRepository;
using StudioGameService.DB.Context;
using StudioGameService.DB.Model;
using StudioGameService.Repository.Interfaces;

namespace StudioGameService.Repository
{
    public class TagRepository : GenericRepository<Tag, int>, ITagRepository
    {
        public TagRepository(ApplicationContext context) : base(context)
        {
        }
    }
}

using Library.Generics.GenericRepository;
using UserActivityService.DB.Context;
using UserActivityService.DB.Models;
using UserActivityService.Repository.Interfaces;

namespace UserActivityService.Repository
{
    public class CommentRepository : GenericRepository<Comment, Guid>, ICommentRepository
    {
        public CommentRepository(ApplicationContext context) : base(context)
        { }
    }
}

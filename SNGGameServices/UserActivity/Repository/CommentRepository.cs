using Library.GenericRepository;
using UserActivityService.DB.Context;
using UserActivityService.DB.Models;
using UserActivityService.Repository.Interfaces;

namespace UserActivityService.Repository
{
    public class CommentRepository : GenericRepository<Comment, int>, ICommentRepository
    {
        public CommentRepository(ApplicationContext context) : base(context)
        { }
    }
}

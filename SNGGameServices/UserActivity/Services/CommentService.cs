using UserActivityService.DB.Models;
using UserActivityService.Repository.Interfaces;
using UserActivityService.Services.Interfaces;

namespace UserActivityService.Services
{
    public class CommentService : ICommentService
    {
        protected readonly ICommentRepository commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            this.commentRepository = commentRepository;
        }

        public async Task AddAsync(Comment comment)
        {
            await commentRepository.AddAsync(comment);
            await commentRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var comment = await commentRepository.GetByIdAsync(id);
            if (comment != null)
            {
                commentRepository.DeleteAsync(comment);
                await commentRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<Comment>> GetAllAsync()
        {
            return commentRepository.GetAllAsync();
        }

        public async Task<Comment> GetByIdAsync(int id)
        {
            return await commentRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(Comment comment)
        {
            commentRepository.UpdateAsync(comment);
            await commentRepository.SaveChangesAsync();
        }
    }
}

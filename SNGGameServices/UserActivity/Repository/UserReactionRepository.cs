using Library.Generics.GenericRepository;
using Library.Types;
using Microsoft.EntityFrameworkCore;
using UserActivityService.DB.Context;
using UserActivityService.DB.Models;
using UserActivityService.Repository.Interfaces;

namespace UserActivityService.Repository
{
    public class UserReactionRepository : GenericRepository<UserReaction, int>, IUserReactionRepository
    {
        private DbSet<UserReaction> dbSetUserReaction;
        private DbSet<Comment> dbSetComment;
        public UserReactionRepository(ApplicationContext context) : base(context)
        {
            dbSetComment = context.Set<Comment>();
            dbSetUserReaction = context.Set<UserReaction>();        
        }

        public override async Task AddAsync(params UserReaction[] urs)
        {
            foreach (var userReaction in urs)
            {
                var existingReaction = await dbSetUserReaction
                    .FirstOrDefaultAsync(ur => ur.UserId == userReaction.UserId && ur.CommentId == userReaction.CommentId);

                if (existingReaction != null)
                {
                    throw new ArgumentException($"Пользователь {userReaction.UserId} уже поставил реакцию к комментарию {userReaction.CommentId}");
                }

                var comment = await dbSetComment
                    .FirstOrDefaultAsync(c => c.Id == userReaction.CommentId);

                if (comment == null)
                {
                    throw new ArgumentException($"Комментарий с ID {userReaction.CommentId} не найден.");
                }

                // Увеличиваем счётчик лайков
                comment.CountLike += 1;

                await dbSetUserReaction.AddAsync(userReaction);
            }
        }

        public override async Task DeleteAsync(params UserReaction[] urs)
        {
            foreach (var userReaction in urs)
            {
                var existingReaction = await dbSetUserReaction
                    .Include(ur => ur.Comment) 
                    .FirstOrDefaultAsync(ur => ur.Id == userReaction.Id);

                if (existingReaction == null)
                {
                    throw new ArgumentException($"Реакция с ID {userReaction.Id} не найдена.");
                }

                var comment = existingReaction.Comment;
                if (comment != null)
                {
                    comment.CountLike -= 1;
                }

                dbSetUserReaction.Remove(existingReaction);
            }
        }
    }
}

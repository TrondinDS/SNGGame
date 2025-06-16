using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;

namespace FrontService.Services.Interfaces
{
    public interface ICommentApiService
    {
        Task<IEnumerable<CommentDTO>?> GetAllCommentsAsync();
        Task<CommentDTO?> GetCommentByIdAsync(Guid id);
        Task<CommentDTO?> CreateCommentAsync(CommentCreateDTO commentDto);
        Task<bool> UpdateCommentAsync(Guid id, CommentDTO commentDto);
        Task<bool> DeleteCommentAsync(Guid id);
    }
}

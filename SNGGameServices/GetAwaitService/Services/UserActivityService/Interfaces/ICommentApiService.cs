using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.Comment;

namespace GetAwaitService.Services.UserActivityService.Interfaces
{
    public interface ICommentApiService
    {
        Task<IEnumerable<CommentDTO>?> GetAllAsync();
        Task<CommentDTO?> GetByIdAsync(Guid id);
        Task<CommentDTO?> CreateAsync(CommentDTO dto);
        Task<bool> UpdateAsync(Guid id, CommentDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

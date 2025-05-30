using Library.Generics.DB.DTO.DTOModelServices.UserActivityService.UserReaction;

namespace GetAwaitService.Services.UserActivityService.Interfaces
{
    public interface IUserReactionApiService
    {
        Task<IEnumerable<UserReactionDTO>?> GetAllAsync();
        Task<UserReactionDTO?> GetByIdAsync(Guid id);
        Task<UserReactionDTO?> CreateAsync(UserReactionDTO dto);
        Task<bool> UpdateAsync(Guid id, UserReactionDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

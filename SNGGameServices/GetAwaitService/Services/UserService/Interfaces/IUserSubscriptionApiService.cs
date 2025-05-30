using Library.Generics.DB.DTO.DTOModelServices.UserService.UserSubscription;

namespace GetAwaitService.Services.UserService.Interfaces
{
    public interface IUserSubscriptionApiService
    {
        Task<IEnumerable<UserSubscriptionDTO>?> GetAllAsync();
        Task<UserSubscriptionDTO?> GetByIdAsync(Guid id);
        Task<UserSubscriptionDTO?> CreateAsync(UserSubscriptionCreateDTO dto);
        Task<bool> UpdateAsync(Guid id, UserSubscriptionDTO dto);
        Task<bool> DeleteAsync(Guid id);
    }
}

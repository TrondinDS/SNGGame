using UserService.DB.Models;

namespace UserService.Services.Interfaces
{
    public interface IUserService
    {
        Task AddAsync(User customer);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task UpdateAsync(User customer);
    }
}

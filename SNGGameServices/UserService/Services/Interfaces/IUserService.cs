using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using UserService.DB.Models;

namespace UserService.Services.Interfaces
{
    public interface IUserService
    {
        Task AddAsync(UserDTO customer);
        Task DeleteAsync(Guid id);
        Task<IEnumerable<UserDTO>> GetAllAsync();
        Task<UserDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(UserDTO customer);
    }
}

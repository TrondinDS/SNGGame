using Library.Generics.DB.DTO.DTOModelServices.UserService.User;

namespace FrontService.Services.Interfaces
{
    public interface IUserApiService
    {
        Task<IEnumerable<UserDTO>?> GetAllUsersAsync();
        Task<UserDTO?> GetUserByIdAsync(Guid id);
        Task<UserDTO?> CreateUserAsync(UserCreateDTO userDto);
        Task<bool> UpdateUserAsync(Guid id, UserDTO userDto);
        Task<bool> DeleteUserAsync(Guid id);
    }
}

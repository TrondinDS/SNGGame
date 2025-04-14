using GetAwaitService.DB.Models;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;

namespace GetAwaitService.Auth.JWT.Service
{
    public interface IAuthServiceJWT
    {
        public Task<string> GenerateJWT(Guid userId, int userTelegramId);
        public Task<string> Login(int userTelegramId);
        public Task<UserTelegramInformation> SearchUser(int userTelegramId);
        public Task<UserTelegramInformation> CreateUserTG(int userTelegramId, UserDTO userDTO);
        Task<UserDTO> CreateUserAsync(UserCreateDTO userDto);
    }
}

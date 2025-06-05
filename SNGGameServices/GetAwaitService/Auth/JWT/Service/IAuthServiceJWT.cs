using GetAwaitService.DB.Models;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;

namespace GetAwaitService.Auth.JWT.Service
{
    public interface IAuthServiceJWT
    {
        public Task<string> GenerateJWT(Guid userId, ulong userTelegramId);
        public Task<string> Login(ulong userTelegramId);
        public Task<UserTelegramInformation> SearchUser(ulong userTelegramId);
        public Task<UserTelegramInformation> CreateUserTG(ulong userTelegramId, UserDTO userDTO);
        Task<UserDTO> CreateUserAsync(UserCreateDTO userDto);
    }
}

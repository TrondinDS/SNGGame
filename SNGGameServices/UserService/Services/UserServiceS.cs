using UserService.DB.Models;
using UserService.Repository.Interfaces;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class UserServiceS : IUserService
    {
        protected readonly IUserRepository userRepository;

        public UserServiceS(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task AddAsync(User customer)
        {
            await userRepository.AddAsync(customer);
            await userRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user != null)
            {
                userRepository.DeleteAsync(user);
                await userRepository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return userRepository.GetAllAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await userRepository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(User customer)
        {
            userRepository.UpdateAsync(customer);
            await userRepository.SaveChangesAsync();
        }
    }
}

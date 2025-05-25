using AutoMapper;
using Library.Generics.DB.DTO.DTOModelServices.StudioGameService.Game;
using Library.Generics.DB.DTO.DTOModelServices.UserService.User;
using Library.Services;
using UserService.DB.Models;
using UserService.Repository.Interfaces;
using UserService.Services.Interfaces;

namespace UserService.Services
{
    public class UserServiceS : IUserService
    {
        protected readonly IUserRepository userRepository;
        private readonly Mongo mongoService;
        private readonly IMapper mapper;

        const string imgsDatabase = "ImagesDatabase";
        const string avasCollection = "GameCollection";

        const string contentDatabase = "ImagesDatabase";
        const string contentCollection = "ContentCollection";

        public UserServiceS(IUserRepository userRepository, Mongo mongoService, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.mongoService = mongoService;
            this.mapper = mapper;
        }

        public async Task AddAsync(UserDTO customer)
        {
            var customerModel = mapper.Map<User>(customer);

            try
            {
                await userRepository.BeginTransactionAsync(); // Начинаем транзакцию

                await userRepository.AddAsync(customerModel);
                await userRepository.SaveChangesAsync(); // Сохраняем в рамках транзакции

                await mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .InsertImg(customerModel.Id, customer.Image, customer.ImageType);

                await mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .InsertStrContent(customerModel.Id, customer.Content);

                await userRepository.CommitTransactionAsync(); // Фиксируем изменения

                customer.Id = customerModel.Id;
            }
            catch (Exception ex)
            {
                await userRepository.RollbackTransactionAsync(); // Откатываем Postgres
                await CompensateAddAsync(customerModel.Id); // Очищаем MongoDB
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }

        private async Task CompensateAddAsync(Guid userId)
        {
            try
            {
                await mongoService.Database(imgsDatabase).Collection(avasCollection)
                    .Delete(userId);

                await mongoService.Database(contentDatabase).Collection(contentCollection)
                    .Delete(userId);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при откате изменений: {ex.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null) return;

            try
            {
                await userRepository.BeginTransactionAsync();

                await userRepository.DeleteAsync(user);
                await userRepository.SaveChangesAsync();

                await mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .Delete(id);

                await mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .Delete(id);

                await userRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await userRepository.RollbackTransactionAsync();
                Console.WriteLine($"Ошибка: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await userRepository.GetAllAsync();

            if (!users.Any())
                return Enumerable.Empty<UserDTO>();

            var tasks = users.Select(user => MapToDTOAsync(user));
            var results = await Task.WhenAll(tasks);

            return results.Where(dto => dto != null);
        }

        private async Task<UserDTO> MapToDTOAsync(User user)
        {
            if (user == null) return null;

            try
            {
                var imageTask = mongoService.Database(imgsDatabase)
                    .Collection(avasCollection)
                    .GetImgById(user.Id);

                var contentTask = mongoService.Database(contentDatabase)
                    .Collection(contentCollection)
                    .GetContentById(user.Id);

                var imageResult = await imageTask;
                var contentResult = await contentTask;

                var dto = mapper.Map<UserDTO>(user);

                if (imageResult != null)
                {
                    dto.Image = imageResult.Bytes;
                    dto.ImageType = imageResult.ContentType;
                }
                else
                {
                    dto.Image = null;
                    dto.ImageType = null;
                }

                dto.Content = contentResult?.Value;

                return dto;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при обработке пользователя с ID {user.Id}: {ex.Message}");
                return null;
            }
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            var user = await userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return await MapToDTOAsync(user);
        }

        public async Task UpdateAsync(UserDTO user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user));

            await userRepository.BeginTransactionAsync();

            try
            {
                var existingUser = await userRepository.GetByIdAsync(user.Id);
                if (existingUser == null)
                    throw new KeyNotFoundException($"Пользователь с ID {user.Id} не найдена.");

                mapper.Map(user, existingUser);
                await userRepository.UpdateAsync(existingUser);
                await userRepository.SaveChangesAsync();

                // Обновляем изображение в MongoDB: Delete + Insert
                if (!string.IsNullOrEmpty(user.Image))
                {
                    var imageBytes = user.Image;

                    await mongoService.Database(imgsDatabase)
                        .Collection(avasCollection)
                        .Delete(user.Id);

                    await mongoService.Database(imgsDatabase)
                        .Collection(avasCollection)
                        .InsertImg(user.Id, imageBytes, user.ImageType);
                }

                // Обновляем контент в MongoDB: Delete + Insert
                if (!string.IsNullOrEmpty(user.Content))
                {
                    await mongoService.Database(contentDatabase)
                        .Collection(contentCollection)
                        .Delete(user.Id);

                    await mongoService.Database(contentDatabase)
                        .Collection(contentCollection)
                        .InsertStrContent(user.Id, user.Content);
                }

                await userRepository.CommitTransactionAsync();
            }
            catch (Exception ex)
            {
                await userRepository.RollbackTransactionAsync();
                Console.WriteLine($"Ошибка при обновлении пользователя: {ex.Message}");
                throw;
            }
        }
    }
}

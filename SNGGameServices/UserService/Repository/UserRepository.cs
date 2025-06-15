using Library.Generics.GenericRepository;
using Microsoft.EntityFrameworkCore;
using UserService.DB.Context;
using UserService.DB.Models;
using UserService.Repository.Interfaces;

namespace UserService.Repository
{
    public class UserRepository : GenericRepository<User, Guid>, IUserRepository
    {
        private DbSet<User> dbUser;
        public UserRepository(ApplicationContext context)
            : base(context)
        {
            dbUser = context.Set<User>();
        }

        /// <summary>
        /// Переопределение метода обновления с проверкой уникальности логина.
        /// Если для хотя бы одного пользователя логин не уникален — метод возвращает null.
        /// </summary>
        public override async Task<bool> UpdateAsync(params User[] entities)
        {
            try
            {
                foreach (var entity in entities)
                {
                    // Проверяем, существует ли пользователь с таким же Login, но другим Id
                    var hasDuplicateLogin = await dbUser
                        .AnyAsync(u => u.Login == entity.Login && u.Id != entity.Id && !u.IsDeleted);

                    if (hasDuplicateLogin)
                    {
                        // Конфликт логина — обновление не выполняется
                        return false;
                    }
                }

                // Если конфликтов нет — вызываем базовую реализацию
                await base.UpdateAsync(entities);
                return true;
            }
            catch (Exception ex)
            {
                // Здесь можно логировать ошибку: например, log.Error(ex, "Ошибка при обновлении пользователей");
                return false;
            }
        }
    }
}

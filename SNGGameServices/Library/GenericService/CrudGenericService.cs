using Library.GenericRepository.Interfaces;
using Library.GenericService.Interfaces;

namespace Library.GenericService
{
    public class CrudGenericService<TEntity, Tid, TRepository> : ICrudGenericService<TEntity>
        where TEntity : class
        where TRepository : IGenericRepository<TEntity, Tid>
    {
        protected readonly TRepository repository;

        public CrudGenericService(TRepository repository)
        {
            this.repository = repository;
        }

        public async Task AddAsync(TEntity entity)
        {
            await repository.AddAsync(entity);
            await repository.SaveChangesAsync();
        }

        public async Task DeleteAsync(object id)
        {
            var user = await repository.GetByIdAsync(id);
            if (user != null)
            {
                repository.DeleteAsync(user);
                await repository.SaveChangesAsync();
            }
        }

        public Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return repository.GetAllAsync();
        }

        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await repository.GetByIdAsync(id);
        }

        public async Task UpdateAsync(TEntity banned)
        {
            repository.UpdateAsync(banned);
            await repository.SaveChangesAsync();
        }
    }
}

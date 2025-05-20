using Library.Generics.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Library.Generics.GenericRepository
{
    public class GenericRepository<TEntity, TId> : IGenericRepository<TEntity, TId>
        where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        private IDbContextTransaction _currentTransaction;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(params TEntity[] entity)
        {
            await _dbSet.AddRangeAsync(entity);
        }

        public virtual async Task UpdateAsync(params TEntity[] entities)
        {
            foreach (var entity in entities)
            {
                var keys = GetEntityKeys(entity);
                var existingEntity = await _dbSet.FindAsync(keys);

                if (existingEntity != null)
                {
                    // Обновляем значения текущей сущности
                    _context.Entry(existingEntity).CurrentValues.SetValues(entity);
                }
                else
                {
                    // Добавляем новую сущность
                    _dbSet.Add(entity);
                }
            }
        }

        private object[] GetEntityKeys(TEntity entity)
        {
            var entityType = _context.Model.FindEntityType(typeof(TEntity));
            var keyProperties = entityType.FindPrimaryKey().Properties;

            return keyProperties.Select(p => p.PropertyInfo.GetValue(entity)).ToArray();
        }

        public virtual async Task DeleteAsync(params TEntity[] entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task BeginTransactionAsync()
        {
            if (_currentTransaction != null) return;

            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction == null) return;

            await _currentTransaction.CommitAsync();
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction == null) return;

            await _currentTransaction.RollbackAsync();
            _currentTransaction.Dispose();
            _currentTransaction = null;
        }
    }
}

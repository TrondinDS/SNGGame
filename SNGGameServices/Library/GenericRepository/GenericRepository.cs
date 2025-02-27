using Library.GenericRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.GenericRepository
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TEntity> _dbSet;

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


        ///ПРОЧЕКАТЬ КАК БУДЕТ РАБОТАТЬ !!!!!
        public virtual async void UpdateAsync(params TEntity[] entities)
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

        ///ПРОЧЕКАТЬ КАК БУДЕТ РАБОТАТЬ !!!!!
        private object[] GetEntityKeys(TEntity entity)
        {
            var entityType = _context.Model.FindEntityType(typeof(TEntity));
            var keyProperties = entityType.FindPrimaryKey().Properties;

            return keyProperties
                .Select(p => p.PropertyInfo.GetValue(entity))
                .ToArray();
        }

        public virtual void DeleteAsync(params TEntity[] entity)
        {
            _dbSet.RemoveRange(entity);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

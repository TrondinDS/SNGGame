using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Generics.GenericRepository.Interfaces
{
    public interface IGenericRepository<TEntity, TId>
        where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(object id);
        Task AddAsync(params TEntity[] entity);
        Task UpdateAsync(params TEntity[] entity);
        Task DeleteAsync(params TEntity[] entity);
        Task SaveChangesAsync();

        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}

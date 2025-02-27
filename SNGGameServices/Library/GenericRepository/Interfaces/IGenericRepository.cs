using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.GenericRepository.Interfaces
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity?> GetByIdAsync(object id);
        Task AddAsync(params TEntity[] entity);
        void UpdateAsync(params TEntity[] entity);
        void DeleteAsync(params TEntity[] entity);
        Task SaveChangesAsync();
    }
}

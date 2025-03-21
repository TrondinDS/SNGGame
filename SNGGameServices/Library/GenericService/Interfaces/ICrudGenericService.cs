using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.GenericService.Interfaces
{
    public interface ICrudGenericService<TEntity>
        where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task DeleteAsync(object id);
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(object id);
        Task UpdateAsync(TEntity entity);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Entities.Abstract;

namespace Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        void Add(T entity);
        T AddWithReturn(T entity);
        void Update(T entity);
        void Remove(T entity);
        void Remove(List<T> entities);
        void SoftRemove(T entity);
        Task AddAsync(T entity);
        Task<T> AddWithReturnAsync(T entity);
        Task<T> GetAsync(Expression<Func<T, bool>> filter);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);
        Task UpdateAsync(T entity);
        Task RemoveAsync(T entity);
        Task RemoveAsync(List<T> entities);
        Task SoftRemoveAsync(T entity);
    }
}

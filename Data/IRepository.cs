using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace RapidPayAPI.Data
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(int id);
        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> expression);
        Task<T> Find(Expression<Func<T, bool>> expression);
    }
}

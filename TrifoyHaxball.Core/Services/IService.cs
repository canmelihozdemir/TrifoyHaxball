using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace TrifoyHaxball.Core.Services
{
    public interface IService<T,T2> where T : class where T2 : class
    {
        Task<T2> GetByIdAsync(int id);
        Task<IEnumerable<T2>> GetAllAsync();
        IQueryable<T> Where(Expression<Func<T,bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T,bool>> expression);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task RemoveAsync(int id);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}

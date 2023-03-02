using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace MyPizza.ApplicationCore.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IList<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null);
        Task<T?> GetByIdAsync(Guid id);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}

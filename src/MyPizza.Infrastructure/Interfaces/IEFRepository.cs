using Microsoft.EntityFrameworkCore.Query;
using MyPizza.ApplicationCore.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyPizza.Infrastructure.Interfaces
{
    public interface IEFRepository<T> : IRepository<T> where T : class
    {
        Task<IList<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null,
                                    Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                    Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
        Task<T?> GetByIdAsync(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
        Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null,
                                     Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
                                     Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null);
    }
}

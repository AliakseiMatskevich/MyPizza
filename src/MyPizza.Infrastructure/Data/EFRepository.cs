using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using MyPizza.ApplicationCore.Entities.Abstracts;
using MyPizza.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MyPizza.Infrastructure.Data
{
    public sealed class EFRepository<T> : IEFRepository<T> where T : BaseEntity
    {
        PizzaContext _dbContext;

        public EFRepository(PizzaContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task CreateAsync(T entity)
        {
            _dbContext.Add(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<T?> GetByIdAsync(Guid id, Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (includes != null)
            { 
                query = includes(query);
            }
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IList<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, 
                Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, 
                Func<IQueryable<T>, IIncludableQueryable<T, object>>? includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (predicate is not null)
            {
                query = query.Where(predicate); 
            }

            if (includes is not null)
            {
                query = includes(query);
            }

            return orderBy is null ? await query.ToListAsync() : await orderBy(query).ToListAsync();
        }

        public async Task<IList<T>> GetListAsync(Expression<Func<T, bool>>? predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            return orderBy is null ? await query.ToListAsync() : await orderBy(query).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}

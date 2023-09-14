using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UfinetPrueba.Domain.Interfaces;

namespace UfinetPrueba.Infrastructure.Repository
{
    public class ApplicationDbContextRepository<T> : IRepository<T> where T : class
    {
        private readonly DbSet<T> _dbSet;

        public ApplicationDbContextRepository(ApplicationDbContext dbContext)
        {
            _dbSet = dbContext.Set<T>();
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _dbSet.AsQueryable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType => _dbSet.AsQueryable().ElementType;

        public Expression Expression => _dbSet.AsQueryable().Expression;

        public IQueryProvider Provider => _dbSet.AsQueryable().Provider;

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
        {
            await _dbSet.AddAsync(entity, cancellationToken);
        }

        public async Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
        {
            _dbSet.Update(entity);
            await Task.CompletedTask; // Puedes implementar la lógica de actualización si es necesario.
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> whereCondition = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = _dbSet;

            if (whereCondition != null)
            {
                query = query.Where(whereCondition);
            }

            // Aquí puedes aplicar la lógica de orderBy y includeProperties si es necesario.

            return Task.FromResult(query.AsEnumerable());
        }
    }
}

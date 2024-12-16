using EmploymentSystemDomain.IRepositoryContract;
using EmploymentSystemInfrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;
using System.Linq.Expressions;

namespace EmploymentSystemInfrastructure.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<T> _dbSet;
        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> CreateAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public bool Delete(T entity)
        {
            _dbSet.Remove(entity);
            return true;
        }

        public async Task<T> Read(Expression<Func<T, bool>>? filter)
        {
            IQueryable<T> query = _dbSet;

            if (filter != null)
                query = query.Where(filter);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> ReadAll(List<Expression<Func<T, bool>>>? filters = null, string includeProperties = "", Expression<Func<T, object>>? orderBy = null, bool? isAscending = true, int pageIndex = 1, int pageSize = 10)
        {
            IQueryable<T> query = _dbSet;

            foreach (var item in filters)
            {
                query = item is null ? query : query.Where(item);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
                query = isAscending is true ? query.OrderBy(orderBy) : query.OrderByDescending(orderBy);

            return await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Entry(entity).State = EntityState.Modified;
            return entity;
        }
    }
}

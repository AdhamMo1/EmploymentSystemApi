using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystemDomain.IRepositoryContract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<IEnumerable<T>> ReadAll(List<Expression<Func<T, bool>>>? filters = null, string includeProperties = "", Expression<Func<T, object>>? orderBy = null, bool? isAscending = true, int pageIndex = 1, int pageSize = 10);
        Task<T> Read(Expression<Func<T, bool>>? filter);
        Task<T> UpdateAsync(T entity);
        bool Delete(T entity);
    }
}

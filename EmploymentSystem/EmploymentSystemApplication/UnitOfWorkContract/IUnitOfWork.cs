using EmploymentSystemDomain.IRepositoryContract;

namespace EmploymentSystemApplication.UnitOfWorkContract
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;
        Task SaveAsync();
    }
}

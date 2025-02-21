namespace EmploymentSystemApplication.ServicesContract.Caching
{
    public interface ICachingService
    {
        Task<T?> GetAsync<T>(string key) where T : class;
        Task SetAsync<T>(string key, T value) where T : class;
        Task RemoveAsync(string key);
        Task RemoveByPrefixAsync(string prefixKey);
    }
}

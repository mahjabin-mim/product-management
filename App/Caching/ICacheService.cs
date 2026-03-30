namespace ProductValidation.Caching
{
    public interface ICacheService
    {
        Task<T> GetOrSetAsync<T>(
            string key,
            Func<Task<T>> getData,
            TimeSpan? absoluteExpireTime = null
        );

        void Remove(string key);
    }
}
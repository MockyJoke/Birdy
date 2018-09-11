using System.Threading.Tasks;

namespace Birdy.Services.Caching
{
    public interface ICachingService<TKey, TValue>
    {
        Task<TValue> GetAsync(TKey key);

        Task SetAsync(TKey key, TValue value);

        Task<bool> HasKeyAsync(TKey key);

        Task DeleteAsync(TKey key);
    }
}
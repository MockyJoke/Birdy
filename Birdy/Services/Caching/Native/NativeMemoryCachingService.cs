using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Birdy.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Birdy.Services.Caching.Native
{
    public class NativeMemoryCachingService<IKey, IValue> : ICachingService<IKey, IValue>
    {
        private object locker;
        private Dictionary<IKey, IValue> cachingDict;
        private LinkedList<IKey> priorityList;
        private MemoryCache cache;
        public int CachingLimit { get; private set; }

        public NativeMemoryCachingService()
        {
            cache = new MemoryCache(new MemoryCacheOptions()
            {
                SizeLimit = 2048,
            });
        }

        public Task DeleteAsync(IKey key)
        {
            cache.Remove(key);
            return Task.CompletedTask;
        }

        public Task<IValue> GetAsync(IKey key)
        {
            return Task.FromResult(cache.Get<IValue>(key));
        }

        public Task<bool> HasKeyAsync(IKey key)
        {
            return Task.FromResult(cache.TryGetValue(key, out IValue value));
        }

        public Task SetAsync(IKey key, IValue value)
        {
            cache.Set(
                key,
                value,
                new MemoryCacheEntryOptions()
                {
                    Size = 1
                }
            );
            return Task.CompletedTask;
        }
    }
}
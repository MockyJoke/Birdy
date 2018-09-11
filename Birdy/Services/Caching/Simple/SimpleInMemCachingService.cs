using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Birdy.Models;

namespace Birdy.Services.Caching.Simple
{
    public class SimpleInMemCachingService<IKey, IValue> : ICachingService<IKey, IValue>
    {
        private object locker;
        private Dictionary<IKey, IValue> cachingDict;
        private LinkedList<IKey> priorityList;
        public int CachingLimit { get; private set; }

        public SimpleInMemCachingService()
        {
            locker = new object();
            cachingDict = new Dictionary<IKey, IValue>();
            priorityList = new LinkedList<IKey>();
            CachingLimit = 500;
        }

        public Task DeleteAsync(IKey key)
        {
            lock (locker)
            {
                priorityList.Remove(key);
                cachingDict.Remove(key);
            }
            return Task.CompletedTask;
        }

        public Task<IValue> GetAsync(IKey key)
        {
            return Task.FromResult(cachingDict[key]);
        }

        public Task<bool> HasKeyAsync(IKey key)
        {
            return Task.FromResult(cachingDict.ContainsKey(key));
        }

        public Task SetAsync(IKey key, IValue value)
        {
            if (cachingDict.ContainsKey(key))
            {
                return Task.CompletedTask;
            }

            lock (locker)
            {
                cachingDict.Add(key, value);
                priorityList.AddFirst(key);
                if (cachingDict.Keys.Count > CachingLimit)
                {
                    CleanUp();
                }
            }
            return Task.CompletedTask;
        }

        private void CleanUp()
        {
            while (cachingDict.Keys.Count > CachingLimit)
            {
                cachingDict.Remove(priorityList.Last.Value);
                priorityList.RemoveLast();
            }
        }
    }
}
using AssemblyBrowser.Application.Contracts;
using System;
using System.Collections.Generic;

namespace AssemblyBrowser.Application.Models
{
    public class CachingService : ICachingService
    {
        private Dictionary<string, object> _assemblyCache;

        public CachingService()
        {
            _assemblyCache = new();
        }

        public void RegisterCache(string key, object cache)
        {
            if (_assemblyCache.TryGetValue(key, out _))
                _assemblyCache.Remove(key);

            _assemblyCache.Add(key, cache);
        }

        public bool TryGetValue(string key, out object cachedObject)
        {
            return _assemblyCache.TryGetValue(key, out cachedObject);
        }

        public object GetOrSearch(string key, Func<object> func)
        {
            object cachedObj;
            if (TryGetValue(key, out cachedObj))
                return cachedObj;

            cachedObj = func.Invoke();
            if (cachedObj == null)
                throw new ArgumentException("The specified function did not retrieved an object.", nameof(func));

            RegisterCache(key, cachedObj);
            return cachedObj;
        }
    }
}

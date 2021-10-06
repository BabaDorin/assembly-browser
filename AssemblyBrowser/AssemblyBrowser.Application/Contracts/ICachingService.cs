using System;

namespace AssemblyBrowser.Application.Contracts
{
    public interface ICachingService
    {
        object GetOrSearch(string key, Func<object> func);
        void RegisterCache(string key, object cache);
        bool TryGetValue(string key, out object cachedObject);
    }
}
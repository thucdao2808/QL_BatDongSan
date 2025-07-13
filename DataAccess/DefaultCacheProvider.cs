using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public interface ICacheProvider
    {
        object Get(string key);
        void Set(string key, object data, double cacheTime);
        void Set(string key, object data, int cacheTime);
        bool IsSet(string key);
        void Invalidate(string key);
        void RemoveAll();
        void RemoveByFirstName(string key);
        string BuildCachedKey(params object[] objects);
    }

    internal class DefaultCacheProvider
    {
        public const double CACHING_TIME_DEFAULT_IN_24_HOURS = 1440D;
        public const double CACHING_TIME_DEFAULT_IN_5_MINUTES = 5D;
        public const double CACHING_TIME_DEFAULT_IN_0_5_MINUTES = 0.5D;
        public const double CACHING_TIME_DEFAULT_IN_0_2_5_MINUTES = 0.25D;//15 second
        public const double CACHING_TIME_DEFAULT_IN_1_MINUTES = 1D; // 1 phut
        public const double CACHING_TIME_DEFAULT_IN_10_MINUTES = 10D; // 10 phut
        public const double CACHING_TIME_DEFAULT_IN_20_MINUTES = 20D;
        public const double CACHING_TIME_DEFAULT_IN_30_MINUTES = 30D;
        public bool IsGettingDataTable { get; set; }
        private ObjectCache Cache
        {
            get { return MemoryCache.Default; }
        }
        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }
        public object Get(string key)
        {
            return Cache[key];
        }
        public void Set(string key, object data, double cacheTime)
        {
            if (data != null)
            {
                var policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);

                if (!string.IsNullOrEmpty(key))
                    Cache.Add(new CacheItem(key, data), policy);
            }
        }
        public void Set(string key, object data, int cacheTime)
        {
            if (data != null)
            {
                var policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.Now + TimeSpan.FromMinutes(cacheTime);

                if (!string.IsNullOrEmpty(key))
                    Cache.Add(new CacheItem(key, data), policy);
            }
        }
        public bool IsSet(string key)
        {
            return (Cache[key] != null);
        }
        public void Invalidate(string key)
        {
            Cache.Remove(key);
        }
        public void RemoveAll()
        {
            try
            {
                List<string> cacheKeys = Cache.Select(kvp => kvp.Key).ToList();
                foreach (string cacheKey in cacheKeys)
                {
                    Cache.Remove(cacheKey);
                }
            }
            catch { }
        }
        public void RemoveByFirstName(string name)
        {
            try
            {
                List<string> cacheKeys =
                    Cache.Where(kvp => kvp.Key.ToLower().Contains(name.ToLower())).Select(kvp => kvp.Key).ToList();
                foreach (string cacheKey in cacheKeys)
                {
                    Cache.Remove(cacheKey);
                }
            }
            catch { }
        }
        public List<string> GetAll()
        {
            var cacheKeys = new List<string>();
            cacheKeys = Cache.Select(kvp => kvp.Key).ToList();
            return cacheKeys;
        }
        public string BuildCachedKey(params object[] objects)
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (objects != null)
            {
                foreach (var item in objects)
                {
                    stringBuilder.AppendFormat("{0}_", item);
                }
            }
            return stringBuilder.ToString().TrimEnd('_');
        }

    }
}

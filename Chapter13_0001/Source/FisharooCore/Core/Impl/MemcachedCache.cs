using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Web;
using System.Web.Caching;
using System.Xml;
using System.Xml.Serialization;
using Enyim.Caching;
using Enyim.Caching.Memcached;
using Fisharoo.FisharooCore.Properties;
using StructureMap;

namespace Fisharoo.FisharooCore.Core.Impl
{
    /// <summary>
    /// A very basic wrapper for System.Web.Caching.Cache
    /// This should be used until we decide if we're going to build a new cache server implementation 
    /// </summary>
    [Pluggable("MemCached")]
    public class MemcachedCache : ICache
    {
        private MemcachedClient cache;

        private TimeSpan _timeSpan = new TimeSpan(
            Settings.Default.DefaultCacheDuration_Days,
            Settings.Default.DefaultCacheDuration_Hours,
            Settings.Default.DefaultCacheDuration_Minutes, 0);

        public MemcachedCache()
        {
            cache = new MemcachedClient();

            List<string> keys = new List<string>();
            cache.Store(StoreMode.Add, "keys", keys);
        }

        /// <summary>
        /// Gets a cache object based on the cache_key.
        /// </summary>
        /// <param name="cache_key"></param>
        /// <returns></returns>
        public object Get(string cache_key)
        {
            return cache.Get(cache_key);
        }

        /// <summary>
        /// Get a list of all the keys in the cache
        /// </summary>
        /// <returns></returns>
        public List<string> GetCacheKeys()
        {
            return cache.Get("keys") as List<string>;
        }

        /// <summary>
        /// Sets a cache_key to a new object.
        /// </summary>
        /// <param name="cache_key"></param>
        /// <param name="cache_object"></param>
        public void Set(string cache_key, object cache_object)
        {
            Set(cache_key, cache_object, _timeSpan);
        }

        /// <summary>
        /// Override to allow expiration at a specific date/time.
        /// </summary>
        /// <param name="cache_key"></param>
        /// <param name="cache_object"></param>
        /// <param name="expiration"></param>
        public void Set(string cache_key, object cache_object, DateTime expiration)
        {
            Set(cache_key, cache_object, expiration, CacheItemPriority.Normal);
        }

        /// <summary>
        /// Override to cache for a specified amount of time.
        /// </summary>
        /// <param name="cache_key"></param>
        /// <param name="cache_object"></param>
        /// <param name="expiration"></param>
        public void Set(string cache_key, object cache_object, TimeSpan expiration)
        {
            Set(cache_key, cache_object, expiration, CacheItemPriority.Normal);
        }

        /// <summary>
        /// Override to allow expiration at a specific date/time and a priority level.
        /// </summary>
        /// <param name="cache_key"></param>
        /// <param name="cache_object"></param>
        /// <param name="expiration"></param>
        /// <param name="priority"></param>
        public void Set(string cache_key, object cache_object, DateTime expiration, CacheItemPriority priority)
        {
            cache.Store(StoreMode.Set, cache_key, cache_object, expiration);
            UpdateKeys(cache_key);
        }

        /// <summary>
        /// Override to cache for a specified amount of time and a priority level.
        /// </summary>
        /// <param name="cache_key"></param>
        /// <param name="cache_object"></param>
        /// <param name="expiration"></param>
        /// <param name="priority"></param>
        public void Set(string cache_key, object cache_object, TimeSpan expiration, CacheItemPriority priority)
        {
            cache.Store(StoreMode.Set, cache_key, cache_object, expiration);
            UpdateKeys(cache_key);
        }

        private void UpdateKeys(string key)
        {
            List<string> keys = new List<string>();
            if (cache.Get("keys") != null)
            {
                keys = cache.Get("keys") as List<string>;
            }

            if (!keys.Contains(key.ToLower()))
            {
                keys.Add(key);
                cache.Store(StoreMode.Set, "keys", keys);
            }
        }

        /// <summary>
        /// Deletes an existing cache object based on a cache_key.
        /// </summary>
        /// <param name="cache_key"></param>
        public void Delete(string cache_key)
        {
            if (Exists(cache_key))
                cache.Remove(cache_key);
        }

        /// <summary>
        /// Returns whether or not a cache_key exists.
        /// </summary>
        /// <param name="cache_key"></param>
        /// <returns></returns>
        public bool Exists(string cache_key)
        {
            if (cache.Get(cache_key) != null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Empties the cache
        /// </summary>
        public void Flush()
        {
            cache.FlushAll();
        }
    }
}
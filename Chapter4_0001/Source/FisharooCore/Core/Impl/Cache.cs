﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Caching;
using Fisharoo.FisharooCore.Properties;

namespace Fisharoo.FisharooCore.Core.Impl
{
    public class Cache
    {
        private static System.Web.Caching.Cache cache;
        private static TimeSpan timeSpan = new TimeSpan(
            Settings.Default.DefaultCacheDuration_Days,
            Settings.Default.DefaultCacheDuration_Hours,
            Settings.Default.DefaultCacheDuration_Minutes, 0);

        static Cache()
        {
            cache = HttpContext.Current.Cache;
        }

        public static object Get(string cache_key)
        {
            return cache.Get(cache_key);
        }

        public static List<string> GetCacheKeys()
        {
            List<string> keys = new List<string>();
            IDictionaryEnumerator ca = cache.GetEnumerator();
            while (ca.MoveNext())
            {
                keys.Add(ca.Key.ToString());
            }
            return keys;
        }

        public static void Set(string cache_key, object cache_object)
        {
            Set(cache_key, cache_object, timeSpan);
        }

        public static void Set(string cache_key, object cache_object, DateTime expiration)
        {
            Set(cache_key, cache_object, expiration, CacheItemPriority.Normal);
        }

        public static void Set(string cache_key, object cache_object, TimeSpan expiration)
        {
            Set(cache_key, cache_object, expiration, CacheItemPriority.Normal);
        }

        public static void Set(string cache_key, object cache_object, DateTime expiration, CacheItemPriority priority)
        {
            cache.Insert(cache_key, cache_object, null, expiration, System.Web.Caching.Cache.NoSlidingExpiration, priority, null);
        }

        public static void Set(string cache_key, object cache_object, TimeSpan expiration, CacheItemPriority priority)
        {
            cache.Insert(cache_key, cache_object, null, System.Web.Caching.Cache.NoAbsoluteExpiration, expiration, priority, null);
        }

        public static void Delete(string cache_key)
        {
            if (Exists(cache_key))
                cache.Remove(cache_key);
        }

        public static bool Exists(string cache_key)
        {
            if (cache[cache_key] != null)
                return true;
            else
                return false;
        }

        public static void Flush()
        {
            foreach (string s in GetCacheKeys())
            {
                Delete(s);
            }
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace abSee.Storage
{
    public class HttpRuntimeCacheStorage : IStorage
    {
        public const string CacheKeyPrefix = "absee-tester-";

        public TimeSpan CacheDuration { get; set; }

        public HttpRuntimeCacheStorage(TimeSpan cacheDuration)
        {
            CacheDuration = cacheDuration;
        }

        private string GetCacheKey(string testname)
        {
            return CacheKeyPrefix + testname;
        }

        public void SaveResults(Entities.ABSeeResult result)
        {
            var results = GetResults(result.TestName);
            results.Add(result);

            lock (results)
            {
                InsertIntoCache(GetCacheKey(result.TestName), results);
            }
        }

        public List<Entities.ABSeeResult> GetResults(string testName)
        {
            throw new NotImplementedException();
        }

        private void InsertIntoCache(string key, object value)
        {
            // use insert instead of add; add fails if the item already exists
            HttpRuntime.Cache.Insert(
                key: key,
                value: value,
                dependencies: null,
                absoluteExpiration: DateTime.Now.Add(CacheDuration), // servers will cache based on local now
                slidingExpiration: System.Web.Caching.Cache.NoSlidingExpiration,
                priority: System.Web.Caching.CacheItemPriority.Low,
                onRemoveCallback: null);
        }
    }
}
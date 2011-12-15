using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Collections;

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

        public void SaveResults(ABSeeResult result)
        {
            var results = GetResults(result.TestName);

            results.RemoveAll(r => r.TesterId == result.TesterId);
            
            results.Add(result);

            lock (results)
            {
                InsertIntoCache(GetCacheKey(result.TestName), results);
            }
        }

        public List<ABSeeResult> GetResults(string testName)
        {
            //Will this even work?
            var result = HttpRuntime.Cache[GetCacheKey(testName)] as List<ABSeeResult>;

            if (result == null)
            {
                result = new List<ABSeeResult>();
            }

            return result;
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

        public List<ABSeeTest> GetActiveTests()
        {
            var results = new List<ABSeeResult>();

            foreach (DictionaryEntry e in HttpRuntime.Cache)
            {
                if (!e.Key.ToString().StartsWith(CacheKeyPrefix))
                    continue;

                var testResults = e.Value as List<ABSeeResult>;
                if (testResults != null)
                {
                    foreach (var c in testResults)
                        results.Add(c);
                }
            }

            var tests = from r in results
                        group r by r.TestName into g
                        select new ABSeeTest
                        {
                            Name = g.Key,
                            StartDate = g.Min(tr => tr.Date)
                        };

            return tests.ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace abSee
{
	/// <summary>
	/// HttpContext based Tester provider
	/// </summary>
	public class WebRequestTesterProvider : ITesterProvider
    {
        private const string CacheKey = ":absee-tester:";

        private ABTester Current
        {
            get
            {
                var context = HttpContext.Current;
                if (context == null) return null;

                return context.Items[CacheKey] as ABTester;
            }
            set
            {
                var context = HttpContext.Current;
                if (context == null) return;

                context.Items[CacheKey] = value;
            }
        }

		public ABTester Start()
		{
			var context = HttpContext.Current;
			if (context == null) return null;

            var url = context.Request.Url;
            var path = context.Request.AppRelativeCurrentExecutionFilePath.Substring(1);

            // don't profile /content or /scripts, either - happens in web.dev
            foreach (var ignored in ABTester.Settings.IgnoredPaths ?? new string[0])
            {
                if (path.ToUpperInvariant().Contains((ignored ?? "").ToUpperInvariant()))
                    return null;
            }

            var result = new ABTester(url.OriginalString);
            Current = result;

            //TODO - User Storage
            //result.User = ABTester.Settings.UserProvider.GetUser(context.Request);

            return result;
		}

		public void Stop(bool discardResults)
		{
			throw new NotImplementedException();
		}

		public ABTester GetCurrentTester()
		{
			return Current;
		}
	}
}

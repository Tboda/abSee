using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using abSee.Storage;
using System.ComponentModel;

namespace abSee
{
	public partial class ABTester
	{
		public static class Settings
		{
            /// <summary>
            /// When <see cref="ABTester.Start"/> is called, if the current request url contains any items in this property,
            /// no tester will be instantiated and no results will be stored.
            /// Default value is { "/absee-", "/content/", "/scripts/", "/favicon.ico" }.
            /// </summary>
            [DefaultValue(new string[] { "/absee-", "/content/", "/scripts/", "/favicon.ico" })]
            public static string[] IgnoredPaths { get; set; }

			public static Storage.IStorage Storage { get; set; }
			public static Storage.IUserProvider UserProvider { get; set; }
			public static ITesterProvider TesterProvider { get; set; }

			public static void EnsureTesterProvider()
			{
				if (TesterProvider == null)
				{
					// Set the default yo...
					TesterProvider = new WebRequestTesterProvider();
				}
                if (Storage == null)
                {
                    //default
                    Storage = new HttpRuntimeCacheStorage(TimeSpan.FromDays(1));
                }
                if (UserProvider == null)
                {
                    //default
                    UserProvider = new IpAddressIdentity();
                }
			}

		}

	}
}

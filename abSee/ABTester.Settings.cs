using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using abSee.Storage;

namespace abSee
{
	public partial class ABTester
	{
		public static class Settings
		{
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
			}

		}

	}
}

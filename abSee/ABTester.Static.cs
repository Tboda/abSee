using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using abSee.Storage;
using System.Runtime.Serialization;

namespace abSee
{
	public partial class ABTester
	{
        /// <summary>
        /// Initiate the Tester
        /// </summary>
        /// <returns></returns>
		public static ABTester Start()
		{
			Settings.EnsureTesterProvider();
			return Settings.TesterProvider.Start();
		}

		public static ABTester Current
		{
			get
			{
				Settings.EnsureTesterProvider();
				return Settings.TesterProvider.GetCurrentTester();
			}
		}
        
        /// <summary>
        /// Run a test
        /// </summary>
        /// <param name="name">The name of the test to run</param>
        /// <param name="options">A list of options</param>
        /// <returns></returns>
		public static string Test(string name, params string[] options)
		{
            if (options.Length < 1) return string.Empty;

            if (Current == null) return options[0];

            return Current.TestImpl(name, options);
		}

        /// <summary>
        /// Triggers a conversion event
        /// </summary>
        /// <param name="name">The name of the test to convert</param>
        public static void Convert(string name)
        {
            if (Current == null) return;

            Current.ConvertImpl(name);
		}

        /// <summary>
        /// Gets results for a given test
        /// </summary>
        /// <param name="name">The name of the test to get results for</param>
        /// <returns></returns>
        public static List<ABSeeResult> GetResults(string name)
        {
            if (Current == null) return new List<ABSeeResult>();

            if (Settings.Storage == null) return new List<ABSeeResult>();

            return Settings.Storage.GetResults(name);
        }

	}
}

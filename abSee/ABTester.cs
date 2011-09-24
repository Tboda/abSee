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
		[DataMember(Order = 1)]
		public Guid Id { get; set; }

		[DataMember(Order = 2)]
		public DateTime Started { get; set; }

		[DataMember(Order = 3)]
		public string MachineName { get; set; }


        /// <summary>
        /// Initiate the Tester
        /// </summary>
        /// <returns></returns>
		public static ABTester Start()
		{
			Settings.EnsureTesterProvider();
			return Settings.TesterProvider.Start();
		}

        /// <summary>
        /// End the testing session
        /// </summary>
        /// <param name="discardResults"></param>
		public static void Stop(bool discardResults = false)
		{
			Settings.EnsureTesterProvider();
			Settings.TesterProvider.Stop(discardResults);
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
        /// Run a test with 2 options
        /// </summary>
        /// <param name="name">The name of the test to run</param>
        /// <param name="option1"></param>
        /// <param name="option2"></param>
        /// <param name="conversion">The name of the conversion event for this test</param>
        /// <returns></returns>
		public static string Test(string name, string option1, string option2, string conversion)
		{
			return Test(name, new string[] { option1, option2 }, conversion);
		}

        /// <summary>
        /// Run a test
        /// </summary>
        /// <param name="name">The name of the test to run</param>
        /// <param name="options">A list of options</param>
        /// <param name="conversion">The name of the conversion event for this test</param>
        /// <returns></returns>
		public static string Test(string name, string[] options, string conversion)
		{
			throw new NotImplementedException();
		}

        /// <summary>
        /// Triggers a conversion event
        /// </summary>
        /// <param name="name"></param>
		public static void Convert(string name)
		{
			throw new NotImplementedException();
		}
	}
}

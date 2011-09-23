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

		[DataMember(Order = 3)]
		public DateTime Started { get; set; }

		[DataMember(Order = 4)]
		public string MachineName { get; set; }



		public static ABTester Start()
		{
			Settings.EnsureTesterProvider();
			return Settings.TesterProvider.Start();
		}

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

		public static string Test(string name, string[] options, string conversion)
		{
			throw new NotImplementedException();
		}

		public static void Conversion(string name)
		{
			throw new NotImplementedException();
		}
	}
}

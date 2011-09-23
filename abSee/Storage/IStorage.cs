using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace abSee.Storage
{
	public interface IStorage
	{
		void SaveResults();
		void GetResults();
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace abSee
{
	public interface ITesterProvider
	{
		ABTester Start();
		void Stop(bool discardResults);
		ABTester GetCurrentTester();
	}
}

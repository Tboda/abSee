using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace abSee
{
	public interface ITesterProvider
	{
		ABTester Start();
		void Stop(bool discardResults);
		ABTester GetCurrentTester();
	}
}

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
		public ABTester Start()
		{
			var context = HttpContext.Current;
			if (context == null) return null;
			
            //something else here

			throw new NotImplementedException();
		}

		public void Stop(bool discardResults)
		{
			throw new NotImplementedException();
		}

		public ABTester GetCurrentTester()
		{
			throw new NotImplementedException();
		}
	}
}

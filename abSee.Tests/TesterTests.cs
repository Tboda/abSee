using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using abSee.Tests.Helpers;

namespace abSee.Tests
{
	/// <summary>
	/// Summary description for UnitTest1
	/// </summary>
	[TestClass]
	public class TesterTests //How many tests can a tester test if a tester test could test testers?
	{
        private HttpContextBase _contextbase;

		public TesterTests()
		{
            _contextbase = Helpers.MvcMockHelpers.FakeHttpContext();
		}

		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

        [TestMethod]
        public void Can_Start_Tester()
        {
            using (var req = MvcMockHelpers.SimulateRequest("http://localhost/Test.aspx"))
            {
                ABTester.Start();

                Assert.IsNotNull(ABTester.Current);
            }
        }

        [TestMethod]
        public void Can_Ouput_Test()
        {
            using (var req = MvcMockHelpers.SimulateRequest("http://localhost/Test.aspx"))
            {
                ABTester.Start();

                Assert.IsNotNull(ABTester.Current);

                var option1 = "one";
                var option2 = "two";

                var output = ABTester.Test("Can_Ouput_Test", option1, option2, "Can_Ouput_Test_Convert");

                Assert.IsTrue(output == option1 || output == option2);
            }
        }

        [TestMethod]
        public void Can_Get_Test_Results()
        {
            using (var req = MvcMockHelpers.SimulateRequest("http://localhost/Test.aspx"))
            {
                var results = ABTester.Settings.Storage.GetResults("Can_Get_Test_Results");

                Assert.IsNotNull(results);
            }
        }

        [TestMethod]
        public void Can_Convert_Test_With_Results()
        {
            using (var req = MvcMockHelpers.SimulateRequest("http://localhost/Test.aspx"))
            {
                ABTester.Start();

                Assert.IsNotNull(ABTester.Current);

                var testname = "Can_Convert_Test";

                var option1 = "one";
                var option2 = "two";

                var convert = "Can_Convert_Test_Convert";

                var output = ABTester.Test(testname, option1, option2, convert);

                Assert.IsTrue(output == option1 || output == option2);

                ABTester.Convert(convert);

                ABTester.Settings.Storage.GetResults(testname);
            }
        }

	}
}

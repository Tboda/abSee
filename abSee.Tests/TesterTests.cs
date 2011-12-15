﻿using System;
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

                var output = ABTester.Test("Can_Ouput_Test", option1, option2);

                Assert.IsTrue(output == option1 || output == option2);
            }
        }

        [TestMethod]
        public void Can_Ouput_Test_With_Five_Options()
        {
            using (var req = MvcMockHelpers.SimulateRequest("http://localhost/Test.aspx"))
            {
                ABTester.Start();

                Assert.IsNotNull(ABTester.Current);

                var option1 = "one";
                var option2 = "two";
                var option3 = "three";
                var option4 = "four";
                var option5 = "five";

                var output = ABTester.Test("Can_Ouput_Test_With_Five_Options", option1, option2, option3, option4, option5);

                var options = new List<string> { option1, option2, option3, option4, option5 };

                Assert.IsTrue(options.Contains(output));
            }
        }

        [TestMethod]
        public void Can_Get_Test_Results()
        {
            using (var req = MvcMockHelpers.SimulateRequest("http://localhost/Test.aspx"))
            {
                var results = ABTester.GetResults("Can_Get_Test_Results");

                Assert.IsNotNull(results);
            }
        }

        [TestMethod]
        public void Can_Run_Test_With_Results()
        {
            using (var req = MvcMockHelpers.SimulateRequest("http://localhost/Test.aspx"))
            {
                ABTester.Start();

                Assert.IsNotNull(ABTester.Current);

                var testname = "Can_Run_Test_With_Results";

                var option1 = "one";
                var option2 = "two";

                var output = ABTester.Test(testname, option1, option2);

                Assert.IsTrue(output == option1 || output == option2);

                var results = ABTester.GetResults(testname);

                Assert.IsNotNull(results);
                Assert.AreEqual(results.Count, 1);
            }
        }

        [TestMethod]
        public void Can_Run_Test_And_Get_Active_Tests()
        {
            using (var req = MvcMockHelpers.SimulateRequest("http://localhost/Test.aspx"))
            {
                ABTester.Start();

                Assert.IsNotNull(ABTester.Current);

                var testname = "Can_Run_Test_And_Get_Active_Tests";

                var option1 = "one";
                var option2 = "two";

                var output = ABTester.Test(testname, option1, option2);

                Assert.IsTrue(output == option1 || output == option2);

                var results = ABTester.Settings.Storage.GetActiveTests();

                Assert.IsNotNull(results);
                Assert.IsTrue(results.Count > 0);
            }
        }

        [TestMethod]
        public void Can_Convert_Test_With_Results()
        {
            using (var req = MvcMockHelpers.SimulateRequest("http://localhost/Test.aspx"))
            {
                ABTester.Start();

                Assert.IsNotNull(ABTester.Current);

                var testname = "Can_Convert_Test_With_Results";

                var option1 = "one";
                var option2 = "two";

                var output = ABTester.Test(testname, option1, option2);

                Assert.IsTrue(output == option1 || output == option2);

                ABTester.Convert(testname);

                var results = ABTester.GetResults(testname);

                Assert.IsNotNull(results);
                Assert.AreEqual(1, results.Count);
                Assert.IsTrue(results[0].Converted);
            }
        }

        [TestMethod]
        public void Can_Run_Two_Tests_And_Get_One_Result()
        {
            using (var req = MvcMockHelpers.SimulateRequest("http://localhost/Test.aspx"))
            {
                ABTester.Start();

                Assert.IsNotNull(ABTester.Current);

                var testname = "Can_Run_Two_Tests_And_Convert_One";

                var option1 = "one";
                var option2 = "two";

                var output = ABTester.Test(testname, option1, option2);

                Assert.IsTrue(output == option1 || output == option2);

                //Call Start again to similate next request with new ABTester instance
                ABTester.Start();

                var output2 = ABTester.Test(testname, option1, option2);

                Assert.IsTrue(output2 == option1 || output2 == option2);

                ABTester.Convert(testname);

                var results = ABTester.GetResults(testname);

                Assert.IsNotNull(results);
                Assert.AreEqual(1, results.Count);
                Assert.IsTrue(results[0].Converted);
            }
        }

	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace abSee.Storage
{
    /// <summary>
    /// Interface for storing test results
    /// </summary>
	public interface IStorage
	{
        /// <summary>
        /// Saves a set test result
        /// </summary>
		void SaveResults(ABSeeResult result);
        
        /// <summary>
        /// gets test results for a given test
        /// </summary>
        /// <param name="testName">The name of the test to get results for</param>
		List<ABSeeResult> GetResults(string testName);

        /// <summary>
        /// gets all active tests
        /// </summary>
        /// <returns>List of active tests</returns>
        List<ABSeeTest> GetActiveTests();

        /// <summary>
        /// Clears all results for the given test
        /// </summary>
        /// <param name="testName">test to clear results for</param>
        int ClearResults(string testName);

        /// <summary>
        /// Clears all test results
        /// </summary>
        int ClearResults();
	}
}

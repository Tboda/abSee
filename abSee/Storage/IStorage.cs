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
		void SaveResults();
        
        /// <summary>
        /// gets test results for a given test
        /// </summary>
        /// <param name="testName">The name of the test to get results for</param>
		void GetResults(string testName);
	}
}

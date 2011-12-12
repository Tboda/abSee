using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using abSee.Entities;

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
	}
}

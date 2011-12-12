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
		public Guid Id { get; set; }

		public DateTime Started { get; set; }

		public string MachineName { get; set; }

        public string User { get; set; }

        public string Url { get; set; }

        private Random _rand;

        [Obsolete("Used for serialization")]
        public ABTester() { }

        public ABTester(string url)
        {
            Id = Guid.NewGuid();
            MachineName = Environment.MachineName;
            Url = url;

            _rand = new Random();
        }

        internal string TestImpl(string name, string[] options, string conversion)
        {
            //Randomly select which option to use

            var output = options[_rand.Next(options.Length)];
            
            //Store the new test in the IStorage
            //return the selected option
            
            return output;
        }

	}
}

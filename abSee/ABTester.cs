﻿using System;
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

        internal string TestImpl(string name, string[] options)
        {
            //Randomly select which option to use

            var output = options[_rand.Next(options.Length)];
            
            //Store the new test in the IStorage
            var result = new ABSeeResult
            {
                Converted = false,//not yet
                Option = output,
                TesterId = Id,
                TestName = name,
                User = User,
                Date = DateTime.Now
            };

            Settings.Storage.SaveResults(result);

            //return the selected option
            
            return output;
        }

        internal void ConvertImpl(string name)
        {
            var results = Settings.Storage.GetResults(name);

            //TODO - This could be done better, needs testing
            var userResult = results.LastOrDefault(r => r.User == User && !r.Converted);
            //Get the result and update the conversion status
            if (userResult == null) return;

            userResult.Converted = true;
            userResult.ConvertDate = DateTime.Now;

            Settings.Storage.SaveResults(userResult);
        }

	}
}

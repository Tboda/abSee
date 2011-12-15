using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using abSee.Storage;
using System.Runtime.Serialization;
using System.Web;

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
        private HttpContext _context;

        [Obsolete("Used for serialization")]
        public ABTester() { }

        public ABTester(HttpContext context)
        {
            _context = context;

            Id = Guid.NewGuid();
            MachineName = Environment.MachineName;
            Url = _context.Request.Url.AbsoluteUri;

            _rand = new Random();
        }

        internal string GetUserTestOption(string name)
        {
            var testcookie = _context.Request.Cookies["abseetest-" + name];
            if (testcookie == null) return string.Empty;
            return testcookie.Value;
        }

        internal string TestImpl(string name, string[] options)
        {
            var results = Settings.Storage.GetResults(name);

            var userResult = results.LastOrDefault(r => r.User == User && !r.Converted);
            //if the user has a result try get the previous option for the test
            if (userResult != null)
            {
                //Check if the user has already seen this test
                var tempOutput = GetUserTestOption(name);

                if (!string.IsNullOrEmpty(tempOutput))
                {
                    return tempOutput;
                }
            }

            //Randomly select which option to use
            //TODO - Maybe this could be overwritten with an interface or something?
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

            _context.Response.AppendCookie(new HttpCookie("abseetest-" + name, output));

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

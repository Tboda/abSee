using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetBash;
using System.IO;
using NDesk.Options;
using abSee;

namespace abSee.Commands
{
    [WebCommand("absee", "Stuff for abSee")]
    public class abSeeCommand : IWebCommand
    {
        private bool _showHelp;
        private Command _command;

        public string Process(string[] args)
        {
            var sw = new StringWriter();

            var p = new OptionSet() {
                { "r|results",  "displays results for given test \nUSAGE: absee -r [test-name]", 
                  v => _command = Command.results },
                { "l|list",  "displays all running tests", 
                  v => _command = Command.list },
                { "c|clear",  "Clears results for test name (use -A to clear all) \nUSAGE: absee -c [test-name])", 
                  v => _command = Command.clear },
                { "h|help",  "show this message and exit", 
                  v => _showHelp = v != null },
            };

            List<string> extras;
            try
            {
                extras = p.Parse(args);
            }
            catch (OptionException e)
            {
                sw.Write("absee: ");
                sw.WriteLine(e.Message);
                sw.WriteLine("Try `absee --help' for more information.");
                return sw.ToString();
            }

            if (_showHelp || !args.Any())
                return showHelp(p);

            switch (_command)
            {
                case Command.results:
                    return showResults(extras);
                case Command.list:
                    return showList();
                case Command.clear:
                    return showClear(extras.FirstOrDefault());
            }

            throw new ArgumentException("Invalid arguments");
        }

        private string showClear(string testName)
        {
            if (string.IsNullOrWhiteSpace(testName))
                throw new ArgumentNullException("No test name provided use -A for to clear all");

            var sw = new StringWriter();
            int resultsDeleted;

            if (testName.ToUpper() == "-A")
            {
                resultsDeleted = ABTester.Settings.Storage.ClearResults();
                sw.WriteLine(resultsDeleted + " results deleted from all tests");
            }
            else
            {
                resultsDeleted = ABTester.Settings.Storage.ClearResults(testName);
                sw.WriteLine("{0} results deleted from test '{1}'", resultsDeleted, testName);
            }

            return sw.ToString();
        }

        private string showList()
        {
            var tests = ABTester.Settings.Storage.GetActiveTests();

            if (!tests.Any())
                throw new ApplicationException("No active tests");

            var sw = new StringWriter();

            foreach (var t in tests)
            {
                sw.WriteLine(string.Format("{0,-20} {1}", t.Name, t.StartDate.ToString()));
            }

            return sw.ToString();
        }

        private string showResults(List<string> extras)
        {
            var testName = extras.FirstOrDefault();
            var results = ABTester.GetResults(testName);
            var sw = new StringWriter();

            if (!results.Any())
                throw new ApplicationException(string.Format("Test: {0} not found", testName));

            var options = from r in results
                          group r by r.Option into g
                          select new
                          {
                              Option = g.Key,
                              Total = g.Count(),
                              Conversions = g.Count(or => or.Converted)
                          };

            var format = "{0,-18}{1,-12}{2,-12}{3,-12}";

            sw.WriteLine(testName + " Results");
            sw.WriteLine();

            sw.WriteLine(format, "OPTION", "TOTAL", "CONVERT", "%");

            foreach (var o in options)
            {
                var percentage = (double)o.Conversions / (double)o.Total;
                sw.WriteLine(format, o.Option, o.Total, o.Conversions, percentage.ToString("P"));
            }

            sw.WriteLine(format, "TOTAL:", options.Sum(o => o.Total), options.Sum(o => o.Conversions), "");

            return sw.ToString();
        }

        private string showHelp(OptionSet p)
        {
            var sb = new StringWriter();

            sb.WriteLine("Usage: absee [OPTIONS]+");
            sb.WriteLine("Options:");

            p.WriteOptionDescriptions(sb);

            return sb.ToString();
        }

        public bool ReturnHtml
        {
            get { return false; }
        }

        private enum Command
        {
            results,
            list,
            clear
        }
    }
}

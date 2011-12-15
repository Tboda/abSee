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
                { "r|results",  "displays results for given test /n USAGE: absee -r [test-name]", 
                  v => _command = Command.results },
                { "l|list",  "displays all running tests", 
                  v => _command = Command.list },
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
            }

            throw new ArgumentException("Invalid arguments");
        }

        private string showList()
        {
            throw new NotImplementedException();
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
            list
        }
    }
}

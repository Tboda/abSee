using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NetBash;

namespace abSee.Commands
{
    [WebCommand("absee", "Stuff for abSee")]
    public class abSeeCommand : IWebCommand
    {
        public string Process(string[] args)
        {
            if (args.Length > 0)
            {
                return "Need command yo.";
            }

            throw new NotImplementedException();
        }

        public bool ReturnHtml
        {
            get { return true; }
        }
    }
}

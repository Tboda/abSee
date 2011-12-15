using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace abSee
{
	public class ABSeeResult
	{
        public Guid TesterId { get; set; }//?
        public string User { get; set; }//??
        public string TestName { get; set; }
        public string Option { get; set; }
        public bool Converted { get; set; }

        public DateTime Date { get; set; }
        public DateTime? ConvertDate { get; set; }
	}
}

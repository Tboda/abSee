using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace abSee
{
    public interface IOptionSelector
    {
        string SelectOption(HttpContext context, string user, string[] options);
    }
}

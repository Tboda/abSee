using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace abSee
{
    public class RandomOptionSelector : IOptionSelector
    {
        private Random _rand;

        public RandomOptionSelector()
        {
            _rand = new Random();
        }

        public string SelectOption(HttpContext context, string user, string[] options)
        {
            return options[_rand.Next(options.Length)];
        }
    }
}

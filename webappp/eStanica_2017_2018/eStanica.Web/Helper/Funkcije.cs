using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Helper
{
    public class Funkcije
    {
        public static string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("");
            return input.First().ToString().ToUpper() + input.ToLower().Substring(1);
        }
    }
}

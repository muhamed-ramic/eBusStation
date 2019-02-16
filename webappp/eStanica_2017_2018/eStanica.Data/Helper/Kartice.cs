using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Helper
{
    public static class Kartice
    {
        private static List<SelectListItem> Kartice1 { get; set; }
        public static List<SelectListItem> VratiKartice()
        {
            Kartice1 = new List<SelectListItem>();
            Kartice1.Add(new SelectListItem {Text="Master Card", Value="1" });
            Kartice1.Add(new SelectListItem{ Text = "Visa", Value = "2" });
            Kartice1.Add(new SelectListItem{ Text = "Visa Electron", Value = "3" });
            Kartice1.Add(new SelectListItem{ Text = "Credit card", Value = "4" });
            Kartice1.Add(new SelectListItem{ Text = "Maestro", Value = "5" });

            return Kartice1;
        }
    }
}

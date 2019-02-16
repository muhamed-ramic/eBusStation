using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Helper
{
    public static class Banke
    {
        private static List<SelectListItem> BankeSve { get; set; }
        
        public static List<SelectListItem> VratiBanke()
        {
            BankeSve = new List<SelectListItem>();
            BankeSve.Add(new SelectListItem { Text = "Uni Credit", Value = "1" });
            BankeSve.Add(new SelectListItem { Text = "Sparkasse", Value = "2" });
            BankeSve.Add(new SelectListItem { Text = "Intesa", Value = "3" });
            BankeSve.Add(new SelectListItem { Text = "Raiffeisen", Value = "4" });

            return BankeSve;
        }
    }
}

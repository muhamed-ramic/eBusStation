using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace eStanica.Data.Helper
{
    public class Zanimanja
    {
        static List<SelectListItem> Zanim { get; set; }

        public static List<SelectListItem> VratiZanimanja()
        {
            Zanim = new List<SelectListItem>();

            Zanim.Add(new SelectListItem { Text = "Zaposlen", Value = "1" });
            Zanim.Add(new SelectListItem { Text = "Student", Value = "2" });
            Zanim.Add(new SelectListItem { Text = "Penzioner", Value = "3" });
            Zanim.Add(new SelectListItem { Text = "Nezaposlen", Value = "4" });
            return Zanim;
        }

    }
}

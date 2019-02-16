using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Helper
{
    public class ZemljeRegiona
    {
        static List<SelectListItem> Zemlje { get; set; }
        public static List<SelectListItem> VratiZemlje()
        {
            SelectListItem a1 = new SelectListItem
            {
                Text = "BIH",
                Value = "1"
            };
            SelectListItem a2 = new SelectListItem
            {
                Text = "Srbija",
                Value = "2"
            };
            SelectListItem a3 = new SelectListItem
            {
                Text = "Hrvatska",
                Value = "3"
            };
            Zemlje = new List<SelectListItem>();
            Zemlje.Add(a1);
            Zemlje.Add(a2);
            Zemlje.Add(a3);
            return Zemlje;
        }
    }
}

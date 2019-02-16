using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Areas.UpravnikModul.ViewModels
{
    public class LokacijeDodajVM
    {
        public List<SelectListItem> Drzave { get; set; }
        public int DrzavaId { get; set; }
        public int GradId { get; set; }
        public string Naziv { get; set; }
    }
}

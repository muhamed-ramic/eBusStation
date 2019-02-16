using eStanica.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Areas.UpravnikModul.ViewModels
{
    public class LokacijeGradoviVM
    {
        public List<SelectListItem> Drzave { get; set; }
        public int DrzavaId { get; set; }
        public List<Grad> Gradovi { get; set; }

        public string Pretraga { get; set; }
    }
}

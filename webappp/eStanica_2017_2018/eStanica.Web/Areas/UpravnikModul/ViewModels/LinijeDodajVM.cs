using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Areas.UpravnikModul.ViewModels
{
    public class LinijeDodajVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Tip { get; set; }
        public string RedniBr { get; set; }
        public List<SelectListItem> DrzaveP { get; set; }
        public int dpId { get; set; }
        public List<SelectListItem> DrzaveD { get; set; }
        public int ddId { get; set; }
        public List<SelectListItem> Prevoznici { get; set; }
        public int Prevoznik { get; set; }
        public List<SelectListItem> Polasci { get; set; }
        public int Polazak { get; set; }
        public List<SelectListItem> Destinacije { get; set; }
        public int Destinacija { get; set; }
        public string VrijemePolaska { get; set; }
    }
}

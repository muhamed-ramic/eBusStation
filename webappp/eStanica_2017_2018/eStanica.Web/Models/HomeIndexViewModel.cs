using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Models
{
    public class HomeIndexViewModel
    {
        public string Pocetak { get; set; }
        public string Dolazak { get; set; }
        public string Prevoznik { get; set; }
        public int LinijaId { get; set; }
        public string TipLinije { get; set; }
        public bool PostojiPopust { get; set; }
        public string PopustUprocentu { get; set; }
        public string OpisPopusta { get; set; }
        public string VaziDoPopust { get; set; }
        public List<HomeIndexCityVM> homeIndexcityVm { get; set; }
        public HomeIndexViewModel()
        {
            homeIndexcityVm = new List<HomeIndexCityVM>();
        }
    }
    
}

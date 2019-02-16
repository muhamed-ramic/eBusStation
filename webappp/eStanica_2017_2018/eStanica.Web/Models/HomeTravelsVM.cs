using eStanica.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Models
{
    public class HomeTravelsVM
    {
        public string NazivLinije { get; set; }
        public string TipLinije { get; set; }
        public int LinijaId { get; set; }
        public string Prevoznik { get; set; }
        public string Pocetak { get; set; }
        public string Destinacija { get; set; }
        public int BrojGradovaKrozKojeProlazi { get; set; }
        public string VrijemePolaska { get; set; }
        public List<HomeIndexCityVM> ListaGradova { get; set; }
        public bool DaLiImaDruguRutu { get; set; }
        public Karta posjecujeLokacije { get; set; }
        public HomeTravelsVM()
        {
            ListaGradova = new List<HomeIndexCityVM>();
        }
    }
    public class HomeTravelsPrevozniciVM
    {
        public List<HomeTravelsVM> podaci { get; set; }

    }
}

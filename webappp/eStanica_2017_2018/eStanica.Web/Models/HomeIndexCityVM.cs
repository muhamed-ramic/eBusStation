using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Models
{
    public class HomeIndexCityVM
    {
        public string Naziv { get; set; }
        public float Cijena { get; set; }
        public string VrijemeDolsaska { get; set; }
        public string TipKarte { get; set; }
        public int PosjecujeLokacijeId { get; set; }
    }
}

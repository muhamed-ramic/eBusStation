using eStanica.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Models
{
    public class HomeSearchVM
    {
        public List<PosjecujeLokacije> Lokacije { get; set; }
        public List<string> TipKarte { get; set; }
    }
}

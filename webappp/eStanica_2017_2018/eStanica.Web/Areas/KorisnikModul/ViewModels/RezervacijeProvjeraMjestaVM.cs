using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Areas.KorisnikModul.ViewModels
{
    public class RezervacijeProvjeraMjestaVM
    {
        public int BrojZauzetihMjesta { get; set; }
        public int UkupanBrojMjestaAutobusa { get; set; }
        public int BrojSlobodnihMjesta { get; set; }
        public List<int> ZauzetaMjesta { get; set; }
        public List<int> OdabranaMjestaKorisnika { get; set; }
        public int PosjecujeLokacijeId { get; set; }
        public DateTime DatumRezervacije { get; set; }

        public RezervacijeProvjeraMjestaVM()
        {
            ZauzetaMjesta = new List<int>();
            OdabranaMjestaKorisnika = new List<int>();
        }
    }
    public class RezervacijaMjestaJson
    {
        public List<int> Mjesta { get; set; }
    }
}

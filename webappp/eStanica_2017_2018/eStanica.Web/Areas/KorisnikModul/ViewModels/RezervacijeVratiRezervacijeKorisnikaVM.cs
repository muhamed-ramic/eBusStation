using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Areas.KorisnikModul.ViewModels
{
    public class RezervacijeVratiRezervacijeKorisnikaVM
    {
        public string PonudaKupljena { get; set; }
        public string DatumPutovanja { get; set; }
        public string Potroseno { get; set; }
        public int Kolicina { get; set; }
        public bool Aktivna { get; set; }
        public DateTime DatumKupovine { get; set; }
        public string Polazak { get; set; }
        public bool PostojiVecRecenzija { get; set; }
        public int RecenzijaId { get; set; }
        public int TransakcijaId { get; set; }
    }
}

using eStanica.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Areas.KorisnikModul.ViewModels
{
    public class RezervacijePrikazFormePlacanjeVM
    {
        public List<SelectListItem> Karte { get; set; }
        public List<SelectListItem> Banke { get; set; }
        public int OdabranaKartaId { get; set; }
        public int OdabranaBankaId { get; set; }
        public string BrojKreditneKartice { get; set; }
        public string KolicinaKarata { get; set; }
        public float UkupnaCijena { get; set; }
        public float UkupnaCijenaPopust { get; set; }
        public List<int> MjestaOdabrana { get; set; }
        public PosjecujeLokacije PonudaOdabrana { get; set; }
        public string TipKarte { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public RezervacijaMjestaJson ProsliModel { get; set; }
        public bool PostojiPopustNaLiniji { get; set; }
        public bool IstekaoDatumPopusta { get; set; }
        public RezervacijePrikazFormePlacanjeVM()
        {
            MjestaOdabrana = new List<int>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStanica.Data;
using eStanica.Data.Models;
using eStanica.Web.Areas.KorisnikModul.ViewModels;
using eStanica.Web.Helper;
using eUniverzitet.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eStanica.Web.Areas.KorisnikModul.Controllers
{
    [Autorizacija(klijent:true)]
    [Area("KorisnikModul")]
    public class RecenzijeController : Controller
    {
        ApplicationContext _db;
        public RecenzijeController(ApplicationContext db)
        {
            _db = db;
        }
        //Get all recenstion for one travel
        public IActionResult Index(int RecenzijaId=0)
        {
            if (RecenzijaId != 0)
            {
                Recenzija recenzija = _db.Recenzije.Include(x=>x.Karta.PosjecujeLokacije.Grad).Include(x=>x.Karta.PosjecujeLokacije.Linije.Polazak).FirstOrDefault(x => x.Id == RecenzijaId);
                return PartialView("RecenzijaJedna",recenzija);
            }
            return View();
        }
        //View for all users reservation to make recension
        public IActionResult Ocijeni()
        {
            List<RezervacijeVratiRezervacijeKorisnikaVM> viewmodel = new List<RezervacijeVratiRezervacijeKorisnikaVM>();
            Korisnik k = HttpContext.GetLogiraniKorisnik();

            List<Transakcija> transakcije = _db.Transakcije.Where(x => x.KlijentiId == k.Id).ToList();
            foreach (var item in transakcije)
            {
                RezervacijeVratiRezervacijeKorisnikaVM model = new RezervacijeVratiRezervacijeKorisnikaVM();
                TransakcijaStavke stavke = _db.TransakcijaStavke.FirstOrDefault(x => x.TransakcijaId == item.Id);

                if (stavke != null)
                {
                    Karta kartaKupljena = _db.Karte.Include(x => x.PosjecujeLokacije.Linije.Polazak).Include(x => x.PosjecujeLokacije.Grad).FirstOrDefault(x => x.Id == stavke.KartaId);
                    model.DatumPutovanja = kartaKupljena.datumPutovanja.ToShortDateString();
                    model.PonudaKupljena = kartaKupljena.PosjecujeLokacije.Linije.Polazak.Naziv + "-" + kartaKupljena.PosjecujeLokacije.Grad.Naziv;
                    model.Potroseno = stavke.UkupnaCijena.ToString();
                    model.Kolicina = stavke.Kolicina;
                    model.Aktivna = item.otkazano;
                    model.TransakcijaId = item.Id;
                    model.Polazak = kartaKupljena.PosjecujeLokacije.Linije.vrijemePolaska;

                    //Check if user has already made recension
                    if(_db.Recenzije.LastOrDefault(x=>x.KartaId==kartaKupljena.Id && x.KlijentiId == k.Id) != null)
                    {
                        model.PostojiVecRecenzija = true;
                        model.RecenzijaId = _db.Recenzije.LastOrDefault(x => x.KartaId == kartaKupljena.Id && x.KlijentiId == k.Id).Id;
                    }
                    //Get date of traveleing for recenstion
                    model.DatumKupovine = kartaKupljena.datumPutovanja;
                    viewmodel.Add(model);
                }
            }

            return View("VratiRezervacijeTrenutnogKorisnika2",viewmodel);
        }
        //View for recenstion
        public IActionResult FormaZaOcjenu(int transakcijaId)
        {
            ViewBag.TransakcijaId = transakcijaId;
            return PartialView("_FormaZaOcjenu");
        }
        //Logic for evaluating recension
        [HttpPost]
        public IActionResult FormaOcijeni(int transakcijaId,string Ocjena="", string Opis="")
        {
            //Add new recensation
            Recenzija recenzija = new Recenzija();
            List<TransakcijaStavke> transkacija = _db.TransakcijaStavke.Include(x=>x.Karta).Where(x => x.TransakcijaId == transakcijaId).ToList();
            int RecenzijaID = 0;

            foreach (var item in transkacija)
            {
                recenzija.Karta = item.Karta;
                recenzija.Klijenti = HttpContext.GetLogiraniKorisnik() as Klijenti;
                recenzija.Ocjena = Convert.ToInt32(Ocjena);
                recenzija.Opis = Opis;
                _db.Recenzije.Add(recenzija);
            }
            _db.SaveChanges();
            RecenzijaID = _db.Recenzije.LastOrDefault().Id;

            return Redirect("/KorisnikModul/Recenzije/Index?RecenzijaId=" + RecenzijaID);
        }
    }
}
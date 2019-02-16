using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStanica.Data;
using eStanica.Data.Helper;
using eStanica.Data.Models;
using eStanica.Web.Areas.KorisnikModul.ViewModels;
using eStanica.Web.Helper;
using eStanica.Web.Services;
using eUniverzitet.Web.Helper;
using Ispit.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eStanica.Web.Areas.KorisnikModul.Controllers
{
    [Area("KorisnikModul")]
    [Autorizacija(klijent:true)]
    public class RezervacijeController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly IEmailService _emailService;

        public RezervacijeController(ApplicationContext db, IEmailService emailService)
        {
            _db = db;
            _emailService = emailService;
        }
        //View to make reservation at specified date and type of card pick
        public IActionResult Index(int PonudId,string tipKarte="")
        {
            if (string.IsNullOrEmpty(tipKarte))
                ViewBag.NemaKarte = "DA";

            ViewBag.PonudId = PonudId;
            ViewBag.TipKarte = tipKarte;
            return View();
        }
        //Check if place in buses are occupied or free
        [HttpPost]
        public IActionResult ProvjeraMjesta(DateTime Datum, int PonudId, string TipKarte="")
        {
            RezervacijeProvjeraMjestaVM model = new RezervacijeProvjeraMjestaVM();

            HttpContext.Session.SetString("PonudaId", PonudId.ToString());

            MySessionExtensions.Set<DateTime>(HttpContext.Session, "DatumRezervacije", Datum);

            HttpContext.Session.SetString("TipKarte", TipKarte);

            var podaci = _db.PosjecujeLokacije.Include(x => x.Linije.Autobus).FirstOrDefault(x => x.Id == PonudId);
            
            //Get occupied places in bus
            var brojZauzetihMjesta = _db.Karte.Where(x => x.datumPutovanja == Datum && x.PosjecujeLokacije.LinijeId == podaci.LinijeId).ToList();
            foreach (var item in brojZauzetihMjesta)
            {
                model.ZauzetaMjesta.Add(Convert.ToInt32(item.brojSjedista));
            }
            //Initialise model
            model.UkupanBrojMjestaAutobusa = podaci.Linije.Autobus.brojSjedistaBusa;
            model.BrojZauzetihMjesta = brojZauzetihMjesta.Count();
            model.BrojSlobodnihMjesta = model.UkupanBrojMjestaAutobusa - model.BrojZauzetihMjesta;
            
            return View(model);
        }

        //Make reservation of cards
        [HttpPost]
        public IActionResult DodajRezervaciju(RezervacijePrikazFormePlacanjeVM model)
        {
            Korisnik logirani = HttpContext.GetLogiraniKorisnik();

            if (string.IsNullOrEmpty(model.BrojKreditneKartice))
                return Content("niste unijeli kreditnu karticu");
            
            //Add new credit card 
            Kartica novaKartica = new Kartica
            {
                BrojKartice = model.BrojKreditneKartice,
                Klijenti = (Klijenti)logirani,
                SredstvoPlacanjaId = 1,
                Banka = Banke.VratiBanke().Find(x => x.Value == model.OdabranaBankaId.ToString()).Text,
            };
            _db.Kartice.Add(novaKartica);

            //Picked reservation travels
            var ponudaRezervacije = _db.PosjecujeLokacije.Include(x=>x.Linije)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(HttpContext.Session.GetString("PonudaId")));

            //Crete new transaction of cards
            var transakcijaNova = new Transakcija();
            transakcijaNova.brojTransakcije = "1";
            transakcijaNova.datumKupovine = DateTime.Now;
            transakcijaNova.otkazano = false;
            transakcijaNova.Status = "zaprimljena";
            transakcijaNova.Klijenti = (Klijenti)logirani;
            transakcijaNova.Kartica = novaKartica;

            TransakcijaStavke stavkeTransakcije = new TransakcijaStavke();

            //Add new transaction of cards in database
            foreach (var item in model.ProsliModel.Mjesta)
            {
                Karta karta = new Karta();

                stavkeTransakcije.Karta = karta;
                stavkeTransakcije.Kolicina = model.ProsliModel.Mjesta.Count;
                stavkeTransakcije.Transakcija = transakcijaNova;

                if (model.UkupnaCijenaPopust == 0)
                {
                    stavkeTransakcije.UkupnaCijena = model.UkupnaCijena;
                }
                else
                {
                    stavkeTransakcije.UkupnaCijena = model.UkupnaCijenaPopust;
                }

                karta.PosjecujeLokacije = ponudaRezervacije;
                karta.Aktivna = true;
                karta.brojSjedista = item.ToString();
                karta.BrojKarte = "XXX-123";
                karta.datumPutovanja = MySessionExtensions.Get<DateTime>(HttpContext.Session, "DatumRezervacije");

                //karta.datumPutovanja = new DateTime(Convert.ToInt32(godina[0]), Convert.ToInt32(razdvojeniDatum[1]), Convert.ToInt32(razdvojeniDatum[0]));
                karta.TipKarte = _db.TipoviKarte.FirstOrDefault(x => x.Tip == HttpContext.Session.GetString("TipKarte"));

                _db.Karte.Add(karta);
            }
            _db.TransakcijaStavke.Add(stavkeTransakcije);
            _db.SaveChanges();

            //Send user a email
            string bodyText = "Poštovani Uspješno ste platili rezervaciju za ponudu " + ponudaRezervacije.Linije.Naziv + " u iznosu od " + stavkeTransakcije.UkupnaCijena + "na dan " + transakcijaNova.datumKupovine.ToShortDateString() + " Pozdrav Vaša autobuska stanica";
            _emailService.BuildEmailTemplate("Potvrda  o uspješnosti rezervacije", bodyText, logirani.Email);

            return Content("Rezervacija uspjesno provedena");
        }
        //Form for paying
        public IActionResult PrikazFormeZaPlacanje([FromBody]RezervacijaMjestaJson Model)
        {
            var ponudaRezervacije = _db.PosjecujeLokacije
                .FirstOrDefault(x => x.Id == Convert.ToInt32(HttpContext.Session.GetString("PonudaId")));

            //Initializing model
            RezervacijePrikazFormePlacanjeVM viewmodel = new RezervacijePrikazFormePlacanjeVM();
            viewmodel.Banke = Banke.VratiBanke();
            viewmodel.Karte = Kartice.VratiKartice();
            viewmodel.KolicinaKarata = Model.Mjesta.Count.ToString();
            viewmodel.ProsliModel = Model;
            viewmodel.PostojiPopustNaLiniji = _db.PopustNaLiniji.FirstOrDefault(x => x.LinijeId == ponudaRezervacije.LinijeId) != null ? true : false;
            //Check for expiracy
            viewmodel.DatumRezervacije = MySessionExtensions.Get<DateTime>(HttpContext.Session, "DatumRezervacije");

            if (viewmodel.PostojiPopustNaLiniji)
            {
                viewmodel.IstekaoDatumPopusta = _db.PopustNaLiniji.Include(x => x.Popust).FirstOrDefault(x => x.LinijeId == ponudaRezervacije.LinijeId).Popust.DatumVazenjaPopusta > viewmodel.DatumRezervacije ? true : false;
            }
            //Get total spent that user ordered
            foreach (var item in Model.Mjesta)
            {
                viewmodel.UkupnaCijena += _db.PosjecujeLokacije
                .FirstOrDefault(x => x.Id == Convert.ToInt32(HttpContext.Session.GetString("PonudaId"))).cijenaOdPolaska;
                viewmodel.MjestaOdabrana.Add(item);
            }

            //Type of card user picked, travel that he reserved.
            viewmodel.TipKarte = _db.TipoviKarte.FirstOrDefault(x => x.Tip == HttpContext.Session.GetString("TipKarte")).Tip;
            viewmodel.PonudaOdabrana = _db.PosjecujeLokacije.Include(x=>x.Linije.Polazak)
                .Include(x=>x.Grad)
                .FirstOrDefault(x => x.Id == Convert.ToInt32(HttpContext.Session.GetString("PonudaId")));

            return View(viewmodel);
        }
        //Generating code coupon for discount
        public IActionResult GenerisiKod()
        {
            //Generate random code coupon
            string code = Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 6);

            var ponudaRezervacije = _db.PosjecujeLokacije
               .FirstOrDefault(x => x.Id == Convert.ToInt32(HttpContext.Session.GetString("PonudaId")));

            //Set coupon code and discount %
            HttpContext.Session.SetString("code", code);
            HttpContext.Session.SetString("discount", _db.PopustNaLiniji.Include(x=>x.Popust).FirstOrDefault(x=>x.LinijeId==ponudaRezervacije.LinijeId).Popust.Postotak.ToString());
            
            //Send email
            string bodyText = "Postovani vaš kod za uračunavanje popusta je" + code;
            _emailService.BuildEmailTemplate("Kod za uračunavanje popusta na cijenu.", bodyText, HttpContext.GetLogiraniKorisnik().Email);

            return Content("Mail uspjesno poslan sa kodom");
        }
        //Checking validity of code
        public IActionResult ProvjeriValidnostKoda(string code)
        {
            var datumRezervacije = MySessionExtensions.Get<DateTime>(HttpContext.Session, "DatumRezervacije");

            //Check for coupon expiracy
            var ponuda = _db.PosjecujeLokacije.FirstOrDefault(x => x.Id == Convert.ToInt32(HttpContext.Session.GetString("PonudaId")));
            var popust = _db.PopustNaLiniji.Include(x=>x.Popust).FirstOrDefault(x => x.LinijeId == ponuda.LinijeId);

            DateTime datum = datumRezervacije;

            if (HttpContext.Session.GetString("code").Equals(code.Trim()) && datum<popust.Popust.DatumVazenjaPopusta)
            {
                HttpContext.Session.SetString("code", "");
                return Content("Kod je validan");
            }
            return Content("Nevalidan kod");
        }
        //Get all transactions-reservations
        public IActionResult VratiRezervacijeTrenutnogKorisnika()
        {
            List<RezervacijeVratiRezervacijeKorisnikaVM>viewmodel = new List<RezervacijeVratiRezervacijeKorisnikaVM>();
            Korisnik k = HttpContext.GetLogiraniKorisnik();

            List<Transakcija> transakcije = _db.Transakcije.Where(x => x.KlijentiId == k.Id).ToList();
            foreach (var item in transakcije)
            {
                RezervacijeVratiRezervacijeKorisnikaVM model = new RezervacijeVratiRezervacijeKorisnikaVM();
                TransakcijaStavke stavke = _db.TransakcijaStavke.FirstOrDefault(x => x.TransakcijaId == item.Id);

                if (stavke != null)
                {
                    Karta kartaKupljena = _db.Karte.Include(x=>x.PosjecujeLokacije.Linije.Polazak).Include(x=>x.PosjecujeLokacije.Grad).FirstOrDefault(x => x.Id == stavke.KartaId);
                    model.DatumPutovanja = kartaKupljena.datumPutovanja.ToShortDateString();
                    model.PonudaKupljena = kartaKupljena.PosjecujeLokacije.Linije.Polazak.Naziv + "-" + kartaKupljena.PosjecujeLokacije.Grad.Naziv;
                    model.Potroseno = stavke.UkupnaCijena.ToString();
                    model.Kolicina = stavke.Kolicina;
                    model.Aktivna = item.otkazano;
                    model.TransakcijaId = item.Id;
                    model.Polazak = kartaKupljena.PosjecujeLokacije.Linije.vrijemePolaska;
                    viewmodel.Add(model);
                }
            }
            return View(viewmodel);
        }
        //Delete reservation
        public IActionResult IzbrisiRezervaciju(int transakcijaId)
        {
            Transakcija transkacijaZaObrisati = _db.Transakcije.FirstOrDefault(x => x.Id == transakcijaId);
            transkacijaZaObrisati.otkazano = !transkacijaZaObrisati.otkazano;

            List<TransakcijaStavke> stavkeTransakcije = _db.TransakcijaStavke.Where(x => x.TransakcijaId == transkacijaZaObrisati.Id).ToList();
            foreach (var item in stavkeTransakcije)
            {
                _db.TransakcijaStavke.Remove(item);
            }
            _db.Transakcije.Remove(transkacijaZaObrisati);
            _db.SaveChanges();

            return RedirectToAction("VratiRezervacijeTrenutnogKorisnika");
        }
    }
}
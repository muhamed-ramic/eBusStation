using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eStanica.Web.Models;
using eStanica.Data.Models;
using eStanica.Data;
using Microsoft.EntityFrameworkCore;
using eUniverzitet.Web.Helper;
using System.Text.RegularExpressions;

namespace eStanica.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationContext _db;

        public HomeController(ApplicationContext db)
        {
            _db = db;
        }
        //Pocetna stranica
        public IActionResult Index()
        {

            var podaci = _db.Linije.Include(x => x.Destinacija)
                .Include(x => x.Prevoznik)
                .Include(x => x.Polazak)
                .ToList();
            List<HomeIndexViewModel> vm = new List<HomeIndexViewModel>();

            foreach (var item in podaci)
            {
                //Initialising model for landing page
                HomeIndexViewModel m = new HomeIndexViewModel();
                m.Pocetak = item.Polazak.Naziv;
                m.Dolazak = item.Destinacija.Naziv;
                m.Prevoznik = item.Prevoznik.Naziv;
                m.TipLinije = item.TipLinije;
                m.PostojiPopust = _db.PopustNaLiniji.FirstOrDefault(x => x.LinijeId == item.Id) != null ? true : false;
                if (m.PostojiPopust)
                {
                    LinijePopust popust = _db.PopustNaLiniji.Include(x=>x.Popust).FirstOrDefault(x => x.LinijeId == item.Id);
                    m.PopustUprocentu = popust.Popust.Postotak + "%";
                    m.OpisPopusta = popust.Opis;
                    m.VaziDoPopust = popust.Popust.DatumVazenjaPopusta.ToShortDateString();
                }
                m.homeIndexcityVm = _db.PosjecujeLokacije.Include(x => x.Grad)
                     .Where(x => x.LinijeId == item.Id)
                     .Select(x => new HomeIndexCityVM
                     {
                         Naziv = x.Grad.Naziv,
                         VrijemeDolsaska = x.vrijemeDolaska
                     })
                     .ToList();

                //Remove duplicates towns based on card, we have duplicate towns for one lines
                for (int i = 0; i < m.homeIndexcityVm.Count; i++)
                {
                    for (int j = m.homeIndexcityVm.Count - 1; j >= 0; j--)
                    {
                        if (m.homeIndexcityVm[i].Naziv == m.homeIndexcityVm[j].Naziv && i != j)
                        {
                            m.homeIndexcityVm.RemoveAt(j);
                        }
                    }
                }
                vm.Add(m);
            }
            return View(vm);
        }
        //Pregled ponude
        [Autorizacija(klijent: true)]
        public IActionResult Travels(int prevoznikId = 0, string tipKarte = "", int linijaId = 0)
        {
            List<HomeTravelsPrevozniciVM> vm = new List<HomeTravelsPrevozniciVM>();
            List<Prevoznik> sviPrevoznici = new List<Prevoznik>();

            //If we did send search parameter of cardType, search lines by type of card 
            if (tipKarte != "" && linijaId != 0)
            {
                TipKarte tip = _db.TipoviKarte.FirstOrDefault(x => x.Tip.Equals(tipKarte));
                HomeTravelsPrevozniciVM model = new HomeTravelsPrevozniciVM();

                var podaci = _db.Karte.Include(x => x.PosjecujeLokacije.Grad)
                    .Where(x => x.TipKarteId == tip.Id && x.PosjecujeLokacije.LinijeId == linijaId)
                    .ToList();

                //Remove duplicate cards
                var podaciDistinct = podaci.Select(x => x.PosjecujeLokacijeId).Distinct().ToList();
                podaci = podaci.Take(podaciDistinct.Count).ToList();

                // Remove duplicate towns that one line passes, based on card type
                var brojGradovaKrozKojuProlaziLinija = _db.PosjecujeLokacije.Where(x => x.LinijeId == linijaId).ToList();
                var brojGradovaDistinct = brojGradovaKrozKojuProlaziLinija.Select(x => x.GradId).Distinct().ToList();
                brojGradovaKrozKojuProlaziLinija = brojGradovaKrozKojuProlaziLinija.Take(brojGradovaDistinct.Count).ToList();

                HomeTravelsVM submodel = new HomeTravelsVM();
                foreach (var item in podaci)
                {
                    submodel.ListaGradova.Add(new HomeIndexCityVM
                    {
                        Cijena = item.PosjecujeLokacije.cijenaOdPolaska,
                        Naziv = item.PosjecujeLokacije.Grad.Naziv,
                        VrijemeDolsaska = item.PosjecujeLokacije.vrijemeDolaska,
                        PosjecujeLokacijeId = item.PosjecujeLokacijeId,
                        TipKarte = tipKarte
                    });
                }
                model.podaci = new List<HomeTravelsVM>();
                model.podaci.Add(submodel);

                vm.Add(model);
                return PartialView("_Travels", vm);
            }

            //If we did not send specific traveler.
            if (prevoznikId == 0)
            {
                sviPrevoznici = _db.Prevoznici.ToList();
            }

            //Get specific traveller
            else
            {
                sviPrevoznici.Add(_db.Prevoznici.Find(prevoznikId));
            }

            foreach (var item in sviPrevoznici)
            {
                HomeTravelsPrevozniciVM m = new HomeTravelsPrevozniciVM();

                m.podaci = _db.Linije.Where(x => x.PrevoznikId == item.Id)
                    .Select(x => new HomeTravelsVM
                    {
                        LinijaId = x.Id,
                        //BrojGradovaKrozKojeProlazi = _db.PosjecujeLokacije.Where(z => z.LinijeId == x.Id && z.Linije.PrevoznikId == item.Id).Count(),
                        NazivLinije = x.Naziv,
                        Pocetak = x.Polazak.Naziv,
                        Destinacija = x.Destinacija.Naziv,
                        DaLiImaDruguRutu = _db.PosjecujeLokacije.Where(z => z.LinijeId == x.Id).Count() != _db.PosjecujeLokacije.Where(z => z.LinijeId == x.Id && z.Linije.PrevoznikId == item.Id).Count() ? true : false,
                        Prevoznik = item.Naziv,
                        TipLinije = x.TipLinije,
                        VrijemePolaska = x.vrijemePolaska,
                    }).ToList();

                vm.Add(m);
            }
            return View(vm);
        }
        //Pretrazivanje funkc
        [Autorizacija(klijent: true)]
        public IActionResult Search(string Pocetak = "", string Destinacija = "", string Prevoznik = "", string VrijemePolaska = "", string VrijemeOdlaksa = "")
        {
            if (string.IsNullOrEmpty(Pocetak) && string.IsNullOrEmpty(Destinacija) && string.IsNullOrEmpty(Prevoznik) && string.IsNullOrEmpty(VrijemePolaska)
                && string.IsNullOrEmpty(VrijemeOdlaksa) || ModelState.IsValid == false)
            {
                return View();
            }
            var regex = new Regex(@"^\d{2}[:.,-\\s]?\d{2}$");
            var regexForNumbers = new Regex(@"^\d+$");
            var regexForText = new Regex(@"^[a-zA-Z]+$");

            //Check for numbers in string(pocetak,destinacija,prevoznik)
           if(string.IsNullOrEmpty(Pocetak)==false || string.IsNullOrEmpty(Destinacija)==false || string.IsNullOrEmpty(Prevoznik) == false)
            {
                //Get not nul string 
                string notNulText = string.IsNullOrEmpty(Pocetak) == false ? Pocetak : Destinacija;
                if (string.IsNullOrEmpty(notNulText))
                    notNulText = Prevoznik;

                if (regexForNumbers.IsMatch(notNulText))
                    return View();
            }

            //Check for string in number field(vrijeme polaska,odlaska)
            if (string.IsNullOrEmpty(VrijemePolaska) == false ||string.IsNullOrEmpty(VrijemeOdlaksa)==false)
            {
                string notNullText = string.IsNullOrEmpty(VrijemePolaska) == false ? VrijemePolaska : VrijemeOdlaksa;

                if (regexForText.IsMatch(notNullText))
                    return View();
            }

            if (ModelState.IsValid)
            {
                HomeSearchVM viewmodel = new HomeSearchVM();
                string vrijemePolaska = "";
                string vrijemeOdlaska = "";

                //Check for numbers in string
                if (string.IsNullOrEmpty(VrijemePolaska) == false)
                {
                    if (regex.IsMatch(VrijemePolaska))
                    {
                        //Find matches of numbers in string 
                        MatchCollection matches = regex.Matches(VrijemePolaska);
                        vrijemePolaska = matches[0].Value;

                        if(vrijemePolaska.Contains(" "))
                        vrijemePolaska = vrijemePolaska.Replace(' ', ':');
                        else if(vrijemePolaska.Contains("."))
                            vrijemePolaska = vrijemePolaska.Replace('.', ':');
                        else if (vrijemePolaska.Contains("-"))
                            vrijemePolaska = vrijemePolaska.Replace('-', ':');


                    }
                }

                var podaci = _db.PosjecujeLokacije.Include(x => x.Linije.Polazak).
              Include(x => x.Grad).Include(x => x.Linije.Prevoznik).Include(x => x.Linije.Destinacija).
              Where(x => x.Linije.Polazak.Naziv.Equals(Pocetak) || x.Grad.Naziv.Equals(Destinacija)
             || x.Linije.Prevoznik.Naziv.Equals(Prevoznik) || x.Linije.vrijemePolaska.Equals(vrijemePolaska) || x.vrijemeDolaska.Equals(vrijemeOdlaska))
              .ToList();

                viewmodel.Lokacije = podaci;
                
                return View("AfterSearch", viewmodel);
            }
            ViewBag.Validacija = "Niste unijeli ispravno podatke";
            return View();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStanica.Data;
using eStanica.Data.Models;
using eUniverzitet.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace eStanica.Web.Areas.UpravnikModul.Controllers
{
    [Area("UpravnikModul")]
    [Autorizacija(uposlenik: true)]
    public class PrevozniciController : Controller
    {

        ApplicationContext _db;
        public PrevozniciController(ApplicationContext db)
        {
            _db = db;
        }


        public IActionResult Prikaz(string Pretraga)
        {
            List<Prevoznik> p = _db.Prevoznici
                .Where(x => Pretraga == null || x.Naziv.Contains(Pretraga))
                .ToList();

            return View(p);
        }

        public IActionResult Izbrisi(int Id)
        {
            Prevoznik g = _db.Prevoznici.Find(Id);
            _db.Prevoznici.Remove(g);
            _db.SaveChanges();

            return RedirectToAction("Prikaz");
        }

        public IActionResult Dodaj()
        {

            return View();
        }

        public IActionResult Uredi(int Id)
        {

            Prevoznik d = _db.Prevoznici.Find(Id);

            return View(d);
        }

        public IActionResult Snimi(Prevoznik d)
        {
            Prevoznik prevoznik;

            if (d.Id == 0)
            {
                prevoznik = new Prevoznik();
                _db.Prevoznici.Add(prevoznik);
            }
            else
            {
                prevoznik = _db.Prevoznici.Find(d.Id);
            }
            prevoznik.Naziv = d.Naziv;
            prevoznik.KontaktTelefon = d.KontaktTelefon;
            prevoznik.webStranica = d.webStranica;

            _db.SaveChanges();

            return RedirectToAction("Prikaz");
        }
    }
}
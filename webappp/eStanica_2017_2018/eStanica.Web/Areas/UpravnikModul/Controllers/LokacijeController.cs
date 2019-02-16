using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStanica.Data;
using eStanica.Data.Models;
using eStanica.Web.Areas.UpravnikModul.ViewModels;
using eStanica.Web.Helper;
using eUniverzitet.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eStanica.Web.Areas.UpravnikModul.Controllers
{
    [Area("UpravnikModul")]
    [Autorizacija(uposlenik: true)]

    public class LokacijeController : Controller
    {
        ApplicationContext _db;
        public LokacijeController(ApplicationContext db)
        {
            _db = db;
        }

        public IActionResult Drzave(string Pretraga)
        {

            List<Drzava> drzave = _db.Drzave
                .Where(x => Pretraga == null || x.Naziv.Contains(Pretraga))
                .ToList();

            if (HttpContext.IsAjax())
                return PartialView(drzave);
            return View(drzave);
        }

        public IActionResult Gradovi(LokacijeGradoviVM VM)
        {
            if (VM.DrzavaId == 0)
                VM.DrzavaId = 1;

            VM.Drzave = _db.Drzave.Select(
                x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }
                ).ToList();
            VM.Gradovi = _db.Gradovi
                .Where(x => x.DrzavaId == VM.DrzavaId && (VM.Pretraga == null || x.Naziv.Contains(VM.Pretraga)))
                .ToList();

            if(HttpContext.IsAjax())
                return PartialView(VM);
            return View(VM);
        }

        public IActionResult Izbrisi(int Id)
        {
            Grad g = _db.Gradovi.Find(Id);
            _db.Gradovi.Remove(g);
            _db.SaveChanges();

            return Redirect("/UpravnikModul/Lokacije/Gradovi?DrzavaId=" + g.DrzavaId);
        }

        public IActionResult Dodaj_Drzavu()
        {

            if (HttpContext.IsAjax())
                return PartialView();
            return View();
        }

        public IActionResult Uredi_Drzavu(int Id)
        {

            Drzava d = _db.Drzave.Find(Id);

            if (HttpContext.IsAjax())
                return PartialView(d);
            return View(d);
        }

        public  IActionResult SnimiDrzavu(Drzava d)
        {
            Drzava drzava;

            if (d.Id == 0)
            {
                drzava = new Drzava();
                _db.Drzave.Add(drzava);
            }
            else
            {
                drzava = _db.Drzave.Find(d.Id);
            }
                drzava.Naziv = d.Naziv;           

            _db.SaveChanges();

            return RedirectToAction("Drzave");
        }

        public IActionResult IzbrisiDrzavu(int Id)
        {
            List<Grad> gradovi = _db.Gradovi.Where(x => x.DrzavaId == Id).ToList();
            foreach (var x in gradovi)
            {
                _db.Remove(x);
            }

            Drzava d = _db.Drzave.Find(Id);
            _db.Drzave.Remove(d);
            _db.SaveChanges();

            return RedirectToAction("Drzave");
        }
        
        public IActionResult Dodaj_Grad(int drzavaId)
        {
            LokacijeDodajVM VM = new LokacijeDodajVM
            {
                Drzave = _db.Drzave.Select
                (
                    x => new SelectListItem
                    {
                        Text = x.Naziv,
                        Value = x.Id.ToString()
                    }
                 ).ToList(),

                DrzavaId = drzavaId
            };

            if (HttpContext.IsAjax())
                return PartialView(VM);
            return View(VM);
        }

        public IActionResult Uredi_Grad(int Id)
        {
            Grad g = _db.Gradovi.Find(Id);

            if (HttpContext.IsAjax())
                return PartialView(g);
            return View(g);
        }

        public IActionResult SnimiGrad(LokacijeDodajVM VM)
        {
            Grad grad = new Grad
            {
                DrzavaId = VM.DrzavaId,
                Naziv = VM.Naziv
            };

            _db.Gradovi.Add(grad);
            _db.SaveChanges();

            return RedirectToAction("Gradovi", new { DrzavaId = grad.DrzavaId });
        }

        public IActionResult SnimiGrad_(Grad g)
        {
            Grad grad = _db.Gradovi.Find(g.Id);
            grad.Naziv = g.Naziv;

            _db.SaveChanges();

            return RedirectToAction("Gradovi", new { DrzavaId = grad.DrzavaId });
        }

    }
}


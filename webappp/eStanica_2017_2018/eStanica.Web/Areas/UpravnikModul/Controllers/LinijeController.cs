using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStanica.Data;
using eStanica.Data.Models;
using eStanica.Web.Areas.UpravnikModul.ViewModels;
using eUniverzitet.Web.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace eStanica.Web.Areas.UpravnikModul.Controllers
{
    [Area("UpravnikModul")]
    [Autorizacija(uposlenik: true)]
    public class LinijeController : Controller
    {

        ApplicationContext _db;
        public LinijeController(ApplicationContext db)
        {
            _db = db;
        }


        public IActionResult Prikaz(string Pretraga)
        {
            List<Linije> p = _db.Linije
                .Include(x => x.Prevoznik)
                .Include(x => x.Destinacija).ThenInclude(x => x.Drzava)
                .Include(x => x.Polazak).ThenInclude(x => x.Drzava)
                .Where(x => Pretraga == null || (
                    x.RedniBrojLinije == Pretraga ||
                    x.Prevoznik.Naziv.Contains(Pretraga) ||
                    x.Polazak.Naziv.Contains(Pretraga) ||
                    x.Destinacija.Naziv.Contains(Pretraga)
                ))
                .ToList();

            return View(p);
        }

        public IActionResult Izbrisi(int Id)
        {
            Linije g = _db.Linije.Find(Id);
            _db.Linije.Remove(g);
            _db.SaveChanges();

            return RedirectToAction("Prikaz");
        }

        public IActionResult Dodaj(LinijeDodajVM VM)
        {
            LinijeDodajVM l = new LinijeDodajVM();

            if(VM.dpId != 0)
            {
                l.dpId = VM.dpId;
            }

            if(VM.ddId != 0)
            {
                l.ddId = VM.ddId;
            }

            if(VM.Polazak != 0)
            {
                l.Polazak = VM.Polazak;
            }

            if(VM.Destinacija != 0)
            {
                l.Destinacija = VM.Destinacija;
            }

            l.DrzaveP = _db.Drzave.Select(
                x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }
                ).ToList();

            l.DrzaveD = _db.Drzave.Select(
                x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }
                ).ToList();

            l.Prevoznici = _db.Prevoznici.Select(
                x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }).ToList();

            l.Polasci = _db.Gradovi.Where(x=>x.DrzavaId==l.dpId).OrderBy(x=>x.Naziv)
                .Select(
                x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }).ToList();

            l.Destinacije = _db.Gradovi.Where(x => x.DrzavaId == l.ddId).OrderBy(x => x.Naziv)
                .Select(
                x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }).ToList();

            return View(l);
        }

        public IActionResult Uredi(int Id)
        {

            Linije d = _db.Linije.Find(Id);

            return View(d);
        }

        public IActionResult Snimi(LinijeDodajVM d)
        {
            Linije l = new Linije();

            if (d.Id == 0)
            {
                l.Naziv = _db.Gradovi.Where(x => x.Id == d.Polazak).Select(x => x.Naziv).FirstOrDefault() + '-' + _db.Gradovi.Where(x => x.Id == d.Destinacija).Select(x => x.Naziv).FirstOrDefault();
                l.PolazakId = d.Polazak;
                l.DestinacijaId = d.Destinacija;
                l.PrevoznikId = d.Prevoznik;
                l.RedniBrojLinije = d.RedniBr.ToString();
                l.TipLinije = d.Tip;
                l.vrijemePolaska = d.VrijemePolaska;

                _db.Linije.Add(l);
            }
            else
            {
                l = _db.Linije.Find(d.Id);
            }
            _db.SaveChanges();

            return RedirectToAction("Prikaz");
        }
    }
}
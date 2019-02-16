using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eStanica.Data.Models;
using eStanica.Data;
using eStanica.Web.Models;
using eUniverzitet.Web.Helper;

namespace eStanica.Web.Controllers
{
    [Area("KorisnikModul")]
    public class PrevozniciController : Controller
    {
        private readonly ApplicationContext _db;
        public PrevozniciController(ApplicationContext db )
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var podaci = _db.Prevoznici.ToList();
            List<PrevozniciIndexViewModel> vm = new List<PrevozniciIndexViewModel>();
            foreach (var item in podaci)
            {
                PrevozniciIndexViewModel m = new PrevozniciIndexViewModel();
                m.KontaktTelefon = item.KontaktTelefon;
                m.Naziv = item.Naziv;
                m.webStranica = item.webStranica;
                m.Id = item.Id;
                vm.Add(m);
            }
            return PartialView(vm);
        }
    }
}
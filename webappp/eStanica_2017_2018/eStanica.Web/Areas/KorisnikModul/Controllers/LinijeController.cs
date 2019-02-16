using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using eStanica.Data.Models;
using eStanica.Data;
using eStanica.Web.Models;
using Microsoft.EntityFrameworkCore;
using eUniverzitet.Web.Helper;

namespace eStanica.Web.Controllers
{
    //[Autorizacija(klijent: true)]
    [Area("KorisnikModul")]
    public class LinijeController : Controller
    {
        ApplicationContext _db;
        public LinijeController(ApplicationContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            var podaci = _db.Linije.Include(x => x.Destinacija)
                .Include(x => x.Polazak)
                .Include(x => x.Prevoznik)
                .Distinct()
                .ToList();
            return PartialView(podaci); 
        }
    }
}
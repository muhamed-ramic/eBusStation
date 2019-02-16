using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using eStanica.Data;
using eStanica.Data.Models;
using eStanica.Web.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using eStanica.Data.Helper;
using System.IO;
using eStanica.Web.Helper;
using System.Configuration;
using eStanica.Web.Services;
using Microsoft.Extensions.Options;

namespace eStanica.Web.Controllers
{

    public class LoginController : Controller
    {
        private readonly ApplicationContext _db;
        private readonly IHostingEnvironment _env;
        private readonly IEmailService _emailService;
        private readonly MyConfig _config;

        SelectListItem a1 = new SelectListItem
        {
            Text = "BIH",
            Value = "1"
        };
        SelectListItem a2 = new SelectListItem
        {
            Text = "Srbija",
            Value = "2"
        };
        SelectListItem a3 = new SelectListItem
        {
            Text = "Hrvatska",
            Value = "3"
        };

        public LoginController(ApplicationContext db, IHostingEnvironment env, IEmailService emailService, IOptions<MyConfig>settings)
        {
            _db = db;
            _env = env;
            _emailService = emailService;
            _config = settings.Value;
        }
        //View za logiranje 
        public IActionResult Index()
        {
            return View(new LoginIndexViewModel());
        }
        //Log out logika
        public IActionResult LogOut()
        {
            HttpContext.SetLogiraniKorisnik(null);
            //HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }
        public IActionResult AfterLogin()
        {
            return View();
        }
        [HttpPost]
        //View za Verifikovanje statusa klijenta(student, penzioner)-Post
        public IActionResult VerifikacijaStatusa(IFormFile pic, int Korisid)
        {
            if (pic != null)
            {
                var stream = new MemoryStream();
                pic.CopyTo(stream);
                Klijenti k = _db.Klijenti.FirstOrDefault(x => x.Id == Korisid);

                k.slikaIndeksa = stream.ToArray();
                _db.Klijenti.Update(k);
                _db.SaveChanges();

            }
            return Content("snimljeno");
        }
        //View za Verifikovanje statusa klijenta(student, penzioner)-Get
        public IActionResult VerifikacijaStatusa()
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            ViewBag.KorisnikIdd = k.Id;

            return View();
        }
        [HttpPost]
        //Logika trazenja korisnika u bazi nakon unosa username i pw u Index Viewu.
        public IActionResult Index(LoginIndexViewModel vm)
        {
            //Trazenje korisnika u bazi

            Korisnik korisnik = new Klijenti();
            korisnik = _db.Korisnici.FirstOrDefault(x => x.username == vm.username && x.password == vm.password);
            if (korisnik != null)
            {
                //Ako je korisnik verifikovao email
                if (korisnik.isValid)
                {
                    HttpContext.SetLogiraniKorisnik(korisnik, vm.Remember);
                    if (korisnik.Zanimanje == "Student" || korisnik.Zanimanje == "Penzioner")
                    {
                        //Castovanje u klijenta 
                        var klijent = korisnik as Klijenti;
                        string zanimanje = klijent.Zanimanje;
                        bool isStudent = false;

                        //Provjera zanimanja i slike
                        isStudent = zanimanje == "Student" ? true : false;
                        if (isStudent)
                        {
                            if (klijent.slikaIndeksa == null)
                            {
                                ViewBag.KorisnikId = korisnik.Id;
                                return PartialView("_Verifikacija");
                            }
                        }
                        else
                        {
                            if (klijent.slikaPenzionerskeKartice == null)
                            {
                                ViewBag.KorisnikId = korisnik.Id;
                                return PartialView("_Verifikacija");
                            }
                        }

                    }
                    return PartialView("_Pocetna");
                }

            }
            return PartialView("_FailureLogin");

        }
        [HttpGet]
        //Sign up view - Get
        public IActionResult SignUp()
        {
            LoginSignUpViewModel vm = new LoginSignUpViewModel();
            vm.Zemlje = new List<SelectListItem>();
            vm.Zemlje.Add(a1);
            vm.Zemlje.Add(a2);
            vm.Zemlje.Add(a3);
            return View(vm);
        }
        public IActionResult Successfull()
        {
            return PartialView();
        }
        [HttpPost]
        //Sign up view - Post
        public IActionResult SignUp2(LoginSignUpViewModel vm)
        {
            List<SelectListItem> zanimanja = Zanimanja.VratiZanimanja();

            if (ModelState.IsValid)
            {
                //Initialise new client
                Klijenti k = new Klijenti();
                k.Ime = vm.Ime;
                k.password = vm.Pw;
                k.Prezime = vm.Prezime;
                k.Spol = vm.Gender[0];
                k.Telefon = vm.Telefon;
                k.username = vm.Username;
                if (vm.OdabranaZemlja == Convert.ToInt32(a1.Value))
                    k.ZemljaPorijekla = a1.Text;
                else if (vm.OdabranaZemlja == Convert.ToInt32(a2.Value))
                    k.ZemljaPorijekla = a2.Text;
                else
                    k.ZemljaPorijekla = a3.Text;
                k.datumRodjenja = vm.DatumRodenja;

                k.Email = vm.Email;
                k.isValid = false;
                foreach (var item in zanimanja)
                {
                    if (vm.odabranoZanimanje == Convert.ToInt32(item.Value))
                        k.Zanimanje = item.Text;
                }
                k.Uloge = _db.Uloge.First();
                _db.Klijenti.Add(k);
                _db.SaveChanges();

                //Email sending
                BuildEmailTemplate(k.Id);
                return PartialView();
            }

            else
            {
                ViewBag.DisableLayout = "DA";
                return View("SignUp", vm);
            }
        }
        public IActionResult Confirm(int regId)
        {
            ViewBag.regId = regId;
            Klijenti klijent = _db.Klijenti.FirstOrDefault(x => x.Id == regId);

            if (klijent != null)
            {
                if(klijent.isValid)
                ViewBag.IsValid = "DA";
            }
            return View();
        }
        public IActionResult RegisterConfirm(int regId)
        {
            //Find user and set him valid in db
            Klijenti k = _db.Klijenti.FirstOrDefault(x => x.Id == regId);
            k.isValid = true;

            _db.Klijenti.Update(k);
            _db.SaveChanges();
            return PartialView();
        }

        public void BuildEmailTemplate(int regId)
        {
            string webRoot = _env.WebRootPath;
            string body = System.IO.File.ReadAllText(webRoot + "/EmailTemplate" + "/Text" + ".cshtml");

            var korisnik = _db.Klijenti.Where(x => x.Id == regId).FirstOrDefault();
            var url = _config.WebHost+"/Confirm?regId=" + regId; 

            body = body.Replace("@ViewBag.ConfirmationLink", url);
            body = body.ToString();

            //Send email using emailService
            _emailService.BuildEmailTemplate("Potvrda korisnickog racuna za autobusku stanicu", body, korisnik.Email);
        }
    }
}
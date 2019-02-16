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
using static eStanica.Web.Areas.UpravnikModul.ViewModels.KorisniciPrikazVM;

namespace eStanica.Web.Areas.UpravnikModul.Controllers
{
    [Area("UpravnikModul")]
    [Autorizacija(uposlenik: true)]
    public class KorisniciController : Controller
    {
        private ApplicationContext _db;

        public KorisniciController(ApplicationContext context)
        {
            _db = context;
        }

        [Autorizacija(uposlenik: false)]
        public IActionResult Prikazi(KorisniciPrikazVM model)
        {
            KorisniciPrikazVM VM = new KorisniciPrikazVM();
            VM.Uloge = _db.Uloge.Select(
                x => new SelectListItem
                {
                    Text = x.Naziv,
                    Value = x.Id.ToString()
                }
                ).ToList();

            if (model.UlogaId != 0)
            {
                VM.UlogaId = model.UlogaId;
            }

            VM.Rows = _db.Korisnici.Where(x => x.UlogeId == VM.UlogaId ||
                    (model.Pretraga != null && (
                        (x.Ime + " " + x.Prezime).Contains(model.Pretraga) ||
                        x.username.Contains(model.Pretraga)
                    ))
                )
                .Select(
                x => new Row
                {
                    Id = x.Id,
                    Ime = x.Ime,
                    Prezime = x.Prezime,
                    Status = x.Discriminator,
                    Username = x.username
                }
                ).ToList();

            if (HttpContext.IsAjax())
                return PartialView(VM);
            return View(VM);
        }

        public IActionResult Profil(int Id)
        {
            Korisnik k;

            if (Id == 0)
            {
                k = HttpContext.GetLogiraniKorisnik();
            }
            else
            {
                Korisnik logirani = HttpContext.GetLogiraniKorisnik();
                bool IsAdmin = logirani.Uloge.Naziv == "Administrator";
                if (logirani.Id == Id || IsAdmin)
                {
                    k = _db.Korisnici.Find(Id);
                }
                else
                {
                    return RedirectToAction("Profil", new { @Id = logirani.Id });
                }
            }

            ProfilVM VM = new ProfilVM
            {
                datumRodjenja = k.datumRodjenja,
                Email = k.Email,
                Ime = k.Ime,
                Prezime = k.Prezime,
                Spol = k.Spol,
                Telefon = k.Telefon,
                ZemljaPorijekla = k.ZemljaPorijekla,
                Id = k.Id,
                Adresa = k.Adresa,
                Grad = k.Grad
            };
            return View(VM);
        }

        [Autorizacija(uposlenik: false)]
        public IActionResult Dodaj(int Id) // UlogaId
        {
            ProfilVM VM = new ProfilVM()
            {
                UlogaId = Id
            };
            return View(VM);

        }


        public IActionResult SnimiProfil(ProfilVM VM)
        {
            Korisnik k;
            Korisnik logirani = HttpContext.GetLogiraniKorisnik();
            bool IsAdmin = logirani.Uloge.Naziv == "Administrator";

            if (VM.Id == 0)
            {
                if (IsAdmin)
                {
                    Uloge u = _db.Uloge.Find(VM.UlogaId);
                    if(u != null)
                    {
                        if (u.Naziv == "Upravnik" || u.Naziv == "Administrator")
                        {
                            Uposlenik up = new Uposlenik();

                            up.username = VM.username;
                            up.password = VM.newpassword;
                            up.UlogeId = VM.UlogaId;
                            up.Ime = VM.Ime;
                            up.Prezime = VM.Prezime;
                            up.Telefon = VM.Telefon;
                            up.datumRodjenja = VM.datumRodjenja;
                            up.Adresa = VM.Adresa;
                            up.ZemljaPorijekla = VM.ZemljaPorijekla;
                            up.Grad = VM.Grad;
                            up.Spol = VM.Spol;
                            up.GodineIskustva = 0;
                            up.isValid = true;
                            up.Email = VM.Email;
                            _db.Uposlenici.Add(up);
                            _db.SaveChanges();

                            TempData["SuccessPorukaIzmjene"] = "Promjene uspješno spašene!";
                            
                            return RedirectToAction("Profil", new { @Id = up.Id });
                        }
                        else
                        {
                            k = new Korisnik();
                            k.UlogeId = VM.UlogaId;
                            _db.Korisnici.Add(k);
                        }
                    }
                    else
                    {
                        return RedirectToAction("Profil", new { @Id = logirani.Id });
                    }
                }
                else
                {
                    return RedirectToAction("Profil", new { @Id = logirani.Id });
                }
            }
            else
            {
                if (logirani.Id == VM.Id || IsAdmin)
                {
                    k = _db.Korisnici.Find(VM.Id);
                }
                else
                {
                    return RedirectToAction("Profil", new { @Id = logirani.Id });
                }
            }

            k.Ime = VM.Ime;
            k.Prezime = VM.Prezime;
            k.Telefon = VM.Telefon;
            k.datumRodjenja = VM.datumRodjenja;
            k.Adresa = VM.Adresa;
            k.ZemljaPorijekla = VM.ZemljaPorijekla;
            k.Grad = VM.Grad;
            k.Spol = VM.Spol;

            _db.SaveChanges();
            TempData["SuccessPorukaIzmjene"] = "Promjene uspješno spašene!";


            return RedirectToAction("Profil", new { @Id = k.Id });
        }

        public IActionResult PromijeniPassword(ProfilVM VM)
        {
            Korisnik logirani = HttpContext.GetLogiraniKorisnik();

            bool IsAdmin = logirani.Uloge.Naziv == "Administrator";

            if (!IsAdmin && VM.Id != logirani.Id)
                return Forbid();

            Korisnik k = _db.Korisnici.Find(VM.Id);

            if (VM.oldPassword == k.password || (IsAdmin && VM.Id != logirani.Id))
            {
                if (VM.newpassword != k.password)
                {
                    k.password = VM.newpassword;
                    TempData["SuccessPoruka"] = "Lozinka uspješno promijenjena!";
                }
                else
                {
                    TempData["ErrorPoruka"] = "Vaša nova lozinka se mora razlikovati od trenutne!";
                }
            }
            _db.SaveChanges();


            return RedirectToAction("Profil", new { @Id = VM.Id });
        }


        public bool ValidirajLozinku(string oldpassword, int Id)
        {
            Korisnik k = HttpContext.GetLogiraniKorisnik();
            if (Id != 0 && Id != k.Id && k.Uloge.Naziv == "Administrator")
                return true;

            if (oldpassword == k.password)
            {
                return true;
            }
            return false;
        }


    }
}
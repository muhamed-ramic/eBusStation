using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using eStanica.Data;
using eStanica.Data.Models;
using eUniverzitet.Web.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace eStanica.Web.Helper
{
    public static class Autentifikacija
    {
        private const string LogiraniKorisnik = "logirani_korisnik";

        public static void SetLogiraniKorisnik(this HttpContext context, Korisnik korisnik, bool remember = false)
        {

            ApplicationContext db = context.RequestServices.GetService<ApplicationContext>();

            string stariToken = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (stariToken != null)
            {
                AutorizacijskiToken obrisati = db.AutorizacijskiToken.FirstOrDefault(x => x.Vrijednost == stariToken);
                if (obrisati != null)
                {
                    db.AutorizacijskiToken.Remove(obrisati);
                    db.SaveChanges();
                }
            }

            if (korisnik != null)
            {

                string token = Guid.NewGuid().ToString();
                db.AutorizacijskiToken.Add(new AutorizacijskiToken
                {
                    Vrijednost = token,
                    KorisnikId = korisnik.Id,
                    VrijemeEvidentiranja = DateTime.Now,
                    IpAdresa = context.Connection.RemoteIpAddress.ToString()
                });
                db.SaveChanges();

                if (remember)
                    context.Response.SetCookieJson(LogiraniKorisnik, token);
                else
                    context.Response.SetCookieJson(LogiraniKorisnik, token, -1);
            }
            else
                context.Response.SetCookieJson(LogiraniKorisnik, null);

        }

        public static string GetTrenutniToken(this HttpContext context)
        {
            return context.Request.GetCookieJson<string>(LogiraniKorisnik);
        }

        public static Korisnik GetLogiraniKorisnik(this HttpContext context)
        {
            ApplicationContext db = context.RequestServices.GetService<ApplicationContext>();

            string token = context.Request.GetCookieJson<string>(LogiraniKorisnik);
            if (token == null)
                return null;

            return db.AutorizacijskiToken
                .Where(x => x.Vrijednost == token)
                .Select(s => s.Korisnik)
                .Include(x => x.Uloge)
                .SingleOrDefault();

        }

        public static bool IsAjax(this HttpContext context)
        {
            return context.Request.Headers["x-requested-with"] == "XMLHttpRequest";
        }

    }
}
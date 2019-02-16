using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStanica.Data;
using eStanica.Data.Models;
using eStanica.Web.Helper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;


namespace eUniverzitet.Web.Helper
{

    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool klijent = false, bool uposlenik = false, bool administrator = true)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { klijent, uposlenik, administrator };
        }
    }


    public class MyAuthorizeImpl : IAsyncActionFilter
    {
        public MyAuthorizeImpl(bool klijent, bool uposlenik,bool administrator)
        {
            _klijent = klijent;
            _uposlenik = uposlenik;
            _administrator = administrator;
        }
        private readonly bool _klijent;
        private readonly bool _uposlenik;
        private readonly bool _administrator;
        public async Task OnActionExecutionAsync(ActionExecutingContext filterContext, ActionExecutionDelegate next)
        {
            Korisnik k = filterContext.HttpContext.GetLogiraniKorisnik();

            if (k == null)
            {
                if (filterContext.Controller is Controller controller)
                {
                    controller.TempData["error_poruka"] = "Niste logirani";
                }

                filterContext.Result = new RedirectToActionResult("Index", "Login", new { @area = "" });
                return;
            }

            //Preuzimamo DbContext preko app services
            ApplicationContext db = filterContext.HttpContext.RequestServices.GetService<ApplicationContext>();

            //klijenti mogu pristupiti 
            if (_klijent && db.Klijenti.Any(s => s.Id == k.Id))
            {
                await next(); //ok - ima pravo pristupa
                return;
            }

            //uposlenici mogu pristupiti 
            if (_uposlenik && db.Uposlenici.Any(s => s.Id == k.Id))
            {
                await next();//ok - ima pravo pristupa
                return;
            }


            if (_administrator && k.Uloge.Naziv=="Administrator")
            {
                await next();//ok - ima pravo pristupa
                return;
            }

            if (filterContext.Controller is Controller c1)
            {
                c1.TempData["error_poruka"] = "Nemate pravo pristupa";
            }
            filterContext.Result = new RedirectToActionResult("Index", "Login", new { @area = "" });
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            // throw new NotImplementedException();
        }
    }
}


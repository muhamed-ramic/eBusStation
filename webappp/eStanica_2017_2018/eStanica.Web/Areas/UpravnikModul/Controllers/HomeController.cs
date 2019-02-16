using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStanica.Web.Helper;
using eUniverzitet.Web.Helper;
using Microsoft.AspNetCore.Mvc;

namespace eStanica.Web.Areas.UpravnikModul.Controllers
{
    [Area("UpravnikModul")]
    [Autorizacija(uposlenik:true)]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            if (HttpContext.IsAjax())
                return PartialView();
            return View();
        }
    }
}
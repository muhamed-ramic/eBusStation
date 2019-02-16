using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Areas.UpravnikModul.ViewModels
{
    public class KorisniciPrikazVM
    {
        public List<Row> Rows;
        public int UlogaId { get; set; }
        public List<SelectListItem> Uloge { get; set; }
        public string Pretraga { get; set; }

        public class Row
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public string Status { get; set; }
        }

    }
}

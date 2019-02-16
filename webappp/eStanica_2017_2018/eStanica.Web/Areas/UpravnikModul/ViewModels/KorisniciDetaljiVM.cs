using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Areas.UpravnikModul.ViewModels
{
    public class KorisniciDetaljiVM
    {
        public List<Row> Rows;

        public class Row
        {
            public int Id { get; set; }
            public string Username { get; set; }
            public string Password { get; set; }
            public string Ime { get; set; }
            public string Prezime { get; set; }
            public DateTime DatumRodjenja { get; set; }
            public string brTel { get; set; }
            public string Status { get; set; }
            public bool Aktivan { get; set; }
        }
    }
}

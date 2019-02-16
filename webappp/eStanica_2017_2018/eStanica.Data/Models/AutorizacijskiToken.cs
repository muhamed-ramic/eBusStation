using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eStanica.Data.Models
{
    public class AutorizacijskiToken
    {
        public int Id { get; set; }
        public string Vrijednost { get; set; }
        public int KorisnikId { get; set; }
        public Korisnik Korisnik { get; set; }
        public DateTime VrijemeEvidentiranja { get; set; }
        public string IpAdresa { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class Korisnik
    {
        public int Id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime datumRodjenja { get; set; }
        public string Email { get; set; }
        public char Spol { get; set; }
        public string Telefon { get; set; }
        public string ZemljaPorijekla { get; set; }
        public bool isValid { get; set; }
        public string Zanimanje { get; set; }
        public virtual Uloge Uloge { get; set; }
        public int UlogeId { get; set; }
        public string Discriminator { get; private set; }
        public string Adresa { get; set; }
        public string Grad { get; set; }
    }
}

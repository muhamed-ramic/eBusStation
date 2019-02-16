using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class Klijenti:Korisnik
    {
        public string BrojIndeksa { get; set; }
        public byte[] slikaIndeksa { get; set; }
        public byte[] slikaPenzionerskeKartice { get; set; }

    }
}

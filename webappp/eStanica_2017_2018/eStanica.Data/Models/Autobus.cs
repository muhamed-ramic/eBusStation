using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class Autobus
    {
        public int Id { get; set; }
        public int brojSjedistaBusa { get; set; }
        public string Proizvodjac { get; set; }
        public byte[] slikaAutobusa { get; set; }
        public virtual VrstaAutobusa VrstaAutobusa { get; set; }
        public int VrstaAutobusaId { get; set; }
    }
}

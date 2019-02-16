using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class Recenzija
    {
        public int Id { get; set; }
        public int Ocjena { get; set; }
        public string Opis { get; set; }
        public string Komentar { get; set; }
        public virtual Klijenti Klijenti { get; set; }
        public int? KlijentiId { get; set; }
        public virtual Karta Karta { get; set; }
        public int? KartaId { get; set; }
        
    }
}

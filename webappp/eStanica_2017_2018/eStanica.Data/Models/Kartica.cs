using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class Kartica
    {
        public int Id { get; set; }
        public string BrojKartice { get; set; }
        public DateTime datumIsteka { get; set; }
        public string Banka { get; set; }
        public string vrstaKartice { get; set; }
        public float StanjeRacuna { get; set; }
        public virtual SredstvoPlacanja SredstvoPlacanja { get; set; }
        public int SredstvoPlacanjaId { get; set; }
        public virtual Klijenti Klijenti { get; set; }
        public int KlijentiId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class Transakcija
    {
        public int Id { get; set; }
        public DateTime datumKupovine { get; set; }
        public string brojTransakcije { get; set; }
        public string Status { get; set; }
        public bool otkazano { get; set; }
        public virtual Klijenti Klijenti { get; set; }
        public int KlijentiId { get; set; }
        public virtual Kartica Kartica{get;set;}
        public int KarticaId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class TransakcijaStavke
    {
        public int Id { get; set; }
        public int Kolicina { get; set; }
        public float UkupnaCijena { get; set; }
        public virtual Transakcija Transakcija { get; set; }
        public int TransakcijaId { get; set; }
        public virtual Karta Karta { get; set; }
        public int KartaId { get; set; }
        public virtual Popust Popust { get; set; }
        public int? PopustId { get; set; }

    }
}

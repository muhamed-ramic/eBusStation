using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class Karta
    {
        public int Id { get; set; }
        public string BrojKarte { get; set; }
        public DateTime datumPutovanja { get; set; }
        public string brojSjedista { get; set; }
        public bool Aktivna { get; set; }
        public virtual TipKarte TipKarte { get; set; }
        public int TipKarteId { get; set; }
        public PosjecujeLokacije PosjecujeLokacije { get; set; }
        public int PosjecujeLokacijeId { get; set; }
    }
}

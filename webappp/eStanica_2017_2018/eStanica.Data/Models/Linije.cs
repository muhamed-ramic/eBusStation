using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace eStanica.Data.Models
{
    public class Linije
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string TipLinije { get; set; }
        public string RedniBrojLinije { get; set; }
        public virtual Prevoznik Prevoznik { get; set; }
        public int PrevoznikId { get; set; }
        public virtual Grad Polazak { get; set; }
        public int PolazakId { get; set; }
        public virtual Grad Destinacija { get; set; }
        public int DestinacijaId { get; set; }
        public string vrijemePolaska { get; set; }
        public virtual Autobus Autobus { get; set; }
        public int? AutobusId { get; set; }
    }
}

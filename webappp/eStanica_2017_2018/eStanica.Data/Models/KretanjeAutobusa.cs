using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class KretanjeAutobusa
    {
        public int Id { get; set; }
        public float TrajanjePuta { get; set; }
        public virtual Linije Linije { get; set; }
        public int LinijeId { get; set; }
        public virtual Autobus Autobus { get; set; }
        public int AutobusId { get; set; }
        public DateTime vrijemePolaska { get; set; }
    }
}

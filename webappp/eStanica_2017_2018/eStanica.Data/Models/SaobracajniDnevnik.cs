using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class SaobracajniDnevnik
    {
        public int Id { get; set; }
        public DateTime vrijemeDolaska { get; set; }
        public DateTime vrijemeOdlaska { get; set; }
        public string brojPutnogNaloga { get; set; }
        public string Zakasnjenje { get; set; }
        public string Primjedbe { get; set; }
        public virtual Uposlenik Uposlenik { get; set; }
        public int UposlenikId { get; set; }
        public virtual Stanica Stanica { get; set; }
        public int StanicaId { get; set; }
    }
}

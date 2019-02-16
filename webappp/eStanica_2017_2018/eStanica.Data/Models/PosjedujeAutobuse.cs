using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class PosjedujeAutobuse
    {
        public int Id { get; set; }
        public virtual Prevoznik Prevoznik { get; set; }
        public int PrevoznikId { get; set;}
        public virtual Autobus Autobus { get; set; }
        public int AutobusId { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class LinijePopust
    {
        public int Id { get; set; }
        public virtual Popust Popust { get; set; }
        public int PopustId { get; set; }
        public virtual Linije Linije { get; set; }
        public int LinijeId { get; set; }
        public string Opis { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class Stanica
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public virtual Grad Grad { get; set; }
        public int GradId { get; set; }
    }
}

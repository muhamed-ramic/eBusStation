using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class Grad
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public virtual Drzava Drzava { get; set; }
        public int DrzavaId { get; set; }
    }
}

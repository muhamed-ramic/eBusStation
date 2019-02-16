using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class PosjecujeLokacije
    {
        public int Id { get; set; }
        public float cijenaOdPolaska { get; set; }
        public virtual Linije Linije { get; set; }
        public int LinijeId { get; set; }
        public virtual Grad Grad { get; set; }
        public int GradId { get; set; }   
        public string vrijemeDolaska { get; set; }
    }
}

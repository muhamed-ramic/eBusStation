using System;
using System.Collections.Generic;
using System.Text;

namespace eStanica.Data.Models
{
    public class Popust
    {
        public int Id { get; set; }
        public string vrstaPopusta { get; set; }
        public float Postotak { get; set; }
        public DateTime DatumVazenjaPopusta { get; set; }
    }
}

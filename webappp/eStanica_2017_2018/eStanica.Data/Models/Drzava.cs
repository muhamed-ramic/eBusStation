using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace eStanica.Data.Models
{
    public class Drzava
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Obavezno polje")]
        [MinLength(3, ErrorMessage = "Naziv mora biti minimalno 3 slova")]
        public string Naziv { get; set; }
    }
}

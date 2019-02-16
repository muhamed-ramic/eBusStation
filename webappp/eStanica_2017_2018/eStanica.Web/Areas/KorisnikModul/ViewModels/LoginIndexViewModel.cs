using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Models
{
    public class LoginIndexViewModel
    {
        [Required(ErrorMessage ="Please enter your password")]
        public string password { get; set; }
        [Required(ErrorMessage ="Please enter your username")]
        public string username { get; set; }
        [Required(ErrorMessage ="Please check your role")]
        public int Uloga { get; set; }
        [Display(Name = "Zapamti me")]
        public bool Remember { get; set; } = true;
    }
}

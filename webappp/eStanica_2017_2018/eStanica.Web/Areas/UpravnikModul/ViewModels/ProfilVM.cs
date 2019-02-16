using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Areas.UpravnikModul.ViewModels
{
    public class ProfilVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Obavezno polje")]
        [StringLength(maximumLength: 25, MinimumLength = 3, ErrorMessage = "Mora biti dužine između 3 i 25 znakova")]
        [RegularExpression(@"[A-Za-z0-9_]*", ErrorMessage = "Dozvoljena su samo slova.")]
        public string username { get; set; }
   
        [Required(ErrorMessage = "Obavezno polje")]
        [EmailAddress(ErrorMessage = "Email adresa nije ispravna.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [RegularExpression(@"[A-Za-z]*", ErrorMessage = "Dozvoljena su samo slova.")]
        public string Ime { get; set; }
        [Required(ErrorMessage = "Obavezno polje")]
        [RegularExpression(@"[A-Za-z]*", ErrorMessage = "Dozvoljena su samo slova.")]
        public string Prezime { get; set; }
        public DateTime datumRodjenja { get; set; }
        public char Spol { get; set; }
        [RegularExpression(@"[+]?[0-9]*", ErrorMessage = "Broj mora biti napisan u formatu '+123456789")]
        [StringLength(15, MinimumLength = 9, ErrorMessage = "Mora biti dužine između 9 i 15 znakova")]
        public string Telefon { get; set; }
        public string ZemljaPorijekla { get; set; }
        public string Grad { get; set; }
        public string Adresa { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [DataType(DataType.Password)]
        [Remote("ValidirajLozinku", "Korisnici", ErrorMessage = "Netačna lozinka", AdditionalFields = "Id")]
        public string oldPassword { get; set; }


        [Required(ErrorMessage = "Obavezno polje")]
        [StringLength(20, ErrorMessage = "Lozinka mora biti dužine između 5 i 20 karaktera", MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string newpassword { get; set; }

        [Required(ErrorMessage = "Obavezno polje")]
        [StringLength(255, ErrorMessage = "Lozinka mora biti dužine između 5 i 20 karaktera", MinimumLength = 5)]
        [DataType(DataType.Password)]
        [Compare("newpassword", ErrorMessage = "Lozinke se ne podudaraju")]
        public string passwordConfirm { get; set; }

        public int UlogaId { get; set; }
    }
}

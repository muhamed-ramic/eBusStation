using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eStanica.Web.Models
{
    public class LoginSignUpViewModel
    {
        [Required(ErrorMessage ="Please enter a valid name;"),DataType(DataType.Text)]
        public string Ime { get; set; }
        [Required(ErrorMessage ="Please enter a valid surname;"),DataType(DataType.Text)]
        public string Prezime { get; set; }
        [Required(ErrorMessage ="Please enter a username"),MinLength(5,ErrorMessage = "Please enter at least 5 characters"),
            MaxLength(20,ErrorMessage = "Maximum is 20 characters"), DataType(DataType.Text,ErrorMessage ="Please enter characters")]
        public string Username { get; set; }
        [DataType(DataType.Password),Required(ErrorMessage ="Please enter a password;"), MinLength(5, ErrorMessage = "Please enter at least 5 characters")
            , MaxLength(20, ErrorMessage = "Maximum is 20 characters")]
        public string Pw { get; set; }
        [DataType(DataType.Password),Required(ErrorMessage ="Please enter again a password;")
            ,Compare("Pw",ErrorMessage ="Password must match;")]
        public string PonovljeniPw { get; set; }
        [DataType(DataType.PhoneNumber,ErrorMessage ="You must eneter a valid phone number;"),Required(ErrorMessage ="Please enter phonenumber;")]
        public string Telefon { get; set; }
        public List<SelectListItem> Zemlje { get; set; }
        public string Gender { get; set; }
        public int OdabranaZemlja { get; set; }
        [Required(ErrorMessage ="You must enter a date of birth;"),DataType(DataType.Date)]
        public DateTime DatumRodenja { get; set; }
        [Required(ErrorMessage ="You must enter email;"),DataType(DataType.EmailAddress,ErrorMessage ="You must enter a valid email adress")]
        public string Email { get; set; }
        public int odabranoZanimanje { get; set; }
    }
    
}

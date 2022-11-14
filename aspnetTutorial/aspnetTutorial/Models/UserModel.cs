using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Models
{
    public class UserModel
    {
        [Required(ErrorMessage = "No ingresaste un nombre")]
        [MaxLength(10)]
        public string Name { get; set; }
        [Required(ErrorMessage = "No ingresaste un apellido")]
        [MaxLength(10)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "No ingresaste correo")]
        [EmailAddress (ErrorMessage = "Correo no válido")]
        public string Email { get; set; }
        [Required(ErrorMessage = "No ingresaste una contraseña")]
        [MinLength(5,ErrorMessage ="ingresa al menos 5 caracteres")]
     
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "No ingresaste una contraseña")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "ingresa al menos 5 caracteres")]
        [Compare("Password", ErrorMessage = "Las contraseñas deben de coincidir")]
        public string ConfirmPassword { get; set;}
        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}

using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Models
{
    public class UserSignInModel
    {   [Required(ErrorMessage = "No ingresaste una contraseña")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage = "ingresa al menos 5 caracteres")]
 
        public string Password { get; set; }
     
        [DataType(DataType.EmailAddress)]

        [Required(ErrorMessage = "No ingresaste correo")]
        [EmailAddress(ErrorMessage = "Correo no válido")]
        public string Email { get; set; }

        [Display(Name="Remember me")]
        public bool RememberMe { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }
    }
}

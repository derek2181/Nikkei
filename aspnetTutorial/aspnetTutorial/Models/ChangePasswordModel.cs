using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "No ingresaste un nombre")]
        public string Name { get; set; }
        [Required(ErrorMessage = "No ingresaste un apellido")]
        public string LastName { get; set; }

     
    }
}

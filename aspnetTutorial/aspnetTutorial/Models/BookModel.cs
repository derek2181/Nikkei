using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using aspnetTutorial.Helpers;
using System.Linq;
using aspnetTutorial.Helpers;
using System.Threading.Tasks;

namespace aspnetTutorial.Models
{
    public class BookModel
    {
        public int People { get; set; }
        public DateTime Date { get; set; }
        public string DateDay { get; set; }

        public string DateHour { get; set; }

        public string userID { get; set; }
      
        [isTrue(ErrorMessage ="Debes aceptar los terminos y condiciones")]
        public bool Terms { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Models
{
    public class EmailConfirmModel
    {
        public string Email { get; set; }
        public bool isConfirmed { get; set; }
        public bool EmailSent { get; set; }

        public bool EmailVerified { get; set; }
    }
}

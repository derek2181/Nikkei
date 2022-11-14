using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Models
{
    public class ReviewModel
    {
        public int ID { get; set; }
        public int rating { get; set; }
        public string review { get; set; }
        public string shortDate { get; set; }
        public string nombreUsuario { get; set; }
        public byte[] imageBinary { get; set; }

        public int horas { get; set; }
        public int dias { get; set; }

        public string finalResult { get; set; }
        public string dishName { get; set; }
        public DateTime? date { get; set; }
    }
}

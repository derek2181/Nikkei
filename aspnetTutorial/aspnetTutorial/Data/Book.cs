using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Data
{
    public class Book
    {
        public int ID { get; set;}
        public DateTime date { get; set; }
        public int people { get; set; }

        public string userID { get; set; }
        public User User { get; set; }

    }
}

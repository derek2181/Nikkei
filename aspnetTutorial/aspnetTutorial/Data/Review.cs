using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Data
{
    public class Review
    {
        public int ID { get; set; }
        
        public int rating { get; set; }
        public string review { get; set; }
        public int dishID { get; set; }
        public DateTime? date { get; set; }

        public string userID { get; set; }
        public User user { get; set; }

        
        public Dish dish { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Data
{
    public class Dish
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public float Price { get; set; }
        public string category { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
        public byte[] imageBinary { get; set; }
  
    }
}

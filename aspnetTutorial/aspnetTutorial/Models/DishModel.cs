using aspnetTutorial.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Models
{
    public class DishModel
    {
       
        public int ID { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
        public int rating { get; set; }
        public float Price { get; set; }
        public byte[] imageBinary { get; set; }

        public int Stars { get; set; }

    }
}

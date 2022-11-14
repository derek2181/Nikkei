using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspnetTutorial.Models
{
    public class ReviewsDishes
    {
        public List<DishModel> Dishes { get; set; }
        public List<ReviewModel> Reviews { get; set; }
    }
}

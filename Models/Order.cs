using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Oltean_Cristina.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int FoodID { get; set; }
        public DateTime OrderDate { get; set; }

        public Customer Customer { get; set; }
        public Food Food { get; set; }
    }
}

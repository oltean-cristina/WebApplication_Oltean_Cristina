using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace WebApplication_Oltean_Cristina.Models
{
    public class Food
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }

        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<DeliveryFood> DeliveryFoods { get; set; }

    }
}

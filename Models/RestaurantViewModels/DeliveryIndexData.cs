using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Oltean_Cristina.Models.RestaurantViewModels
{
    public class DeliveryIndexData
    {
        public IEnumerable<Delivery> Deliveries { get; set; }
        public IEnumerable<Food> Foods { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}

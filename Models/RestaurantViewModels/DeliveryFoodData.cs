using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication_Oltean_Cristina.Models.RestaurantViewModels
{
    public class DeliveryFoodData
    {
        public int FoodID { get; set; }
        public string Name { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDelivery { get; internal set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
namespace WebApplication_Oltean_Cristina.Models.RestaurantViewModels
{
    public class OrderGroup
    {
        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public int FoodCount { get; set; }

    }


}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication_Oltean_Cristina.Models
{
    public class Delivery
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Delivery Name")]
        [StringLength(50)]
        public string DeliveryName { get; set; }

        [StringLength(70)]
        public string Adress { get; set; }
        public ICollection<DeliveryFood> DeliveryFoods { get; set; }
    }
}

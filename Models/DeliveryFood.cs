namespace WebApplication_Oltean_Cristina.Models
{
    public class DeliveryFood
    {
        public int DeliveryID { get; set; }
        public int FoodID { get; set; }
        public Delivery Delivery { get; set; }
        public Food Food { get; set; }

    }
}
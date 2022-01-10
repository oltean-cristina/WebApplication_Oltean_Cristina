using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_Oltean_Cristina.Models;


namespace WebApplication_Oltean_Cristina.Data
{
    public class DbInitializer
    {
        public static void Initialize(RestaurantContext context)
        {
            context.Database.EnsureCreated();
            if (context.Foods.Any())
            {
                return; // BD a fost creata anterior
            }
            var foods = new Food[]
            {
 new Food{Name="Lava Cake",Category="Sweets",Price=Decimal.Parse("13")},
 new Food{Name="Sarmale",Category="Traditional",Price=Decimal.Parse("17")},
 new Food{Name="Taco",Category="Fast Food",Price=Decimal.Parse("10")},
 new Food{Name="Supa e pui",Category="Traditional",Price=Decimal.Parse("12")},
 new Food{Name="Cheesecake",Category="Sweets",Price=Decimal.Parse("9")},
 new Food{Name="Shaorma",Category="Fast Food",Price=Decimal.Parse("14")}
            };
            foreach (Food s in foods)
            {
                context.Foods.Add(s);
            }
            context.SaveChanges();
            var customers = new Customer[]
            {

 new Customer{CustomerID=1050,Name="Abrudan Bogdan",BirthDate=DateTime.Parse("1994-02-01")},
 new Customer{CustomerID=1045,Name="Popescu Mihaela",BirthDate=DateTime.Parse("1987-09-09")},

            };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();
            var orders = new Order[]
            {
new Order{FoodID=1,CustomerID=1050,OrderDate=DateTime.Parse("02-25-2021")},
 new Order{FoodID=3,CustomerID=1045,OrderDate=DateTime.Parse("09-28-2021")},
 new Order{FoodID=1,CustomerID=1045,OrderDate=DateTime.Parse("10-28-2021")},
 new Order{FoodID=2,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2021")},
 new Order{FoodID=4,CustomerID=1050,OrderDate=DateTime.Parse("09-28-2021")},
new Order{FoodID=6,CustomerID=1050,OrderDate=DateTime.Parse("10-28-2021")},
            };
            foreach (Order e in orders)
            {
                context.Orders.Add(e);
            }
            context.SaveChanges();



            var deliveries = new Delivery[]
 {

 new Delivery{DeliveryName="Food Panda",Adress="Str. Aviatorilor, nr. 40,Bucuresti"},
 new Delivery{DeliveryName="Glovo",Adress="Str. Plopilor, nr. 35,Ploiesti"},
 new Delivery{DeliveryName="Tazz",Adress="Str. Cascadelor, nr.22, Cluj-Napoca"},
 };
            foreach (Delivery p in deliveries)
            {
                context.Deliveries.Add(p);
            }
            context.SaveChanges();




            var deliveryfoods = new DeliveryFood[]
             {
 new DeliveryFood {
 FoodID = foods.Single(c => c.Name == "Lava Cake" ).ID,
 DeliveryID = deliveries.Single(i => i.DeliveryName =="Food Panda").ID
 },
 new DeliveryFood {
 FoodID = foods.Single(c => c.Name == "Sarmale" ).ID,
DeliveryID = deliveries.Single(i => i.DeliveryName =="Glovo").ID
 },
 new DeliveryFood {
 FoodID = foods.Single(c => c.Name == "Taco" ).ID,
 DeliveryID = deliveries.Single(i => i.DeliveryName =="Tazz").ID
 },
 new DeliveryFood {
 FoodID = foods.Single(c => c.Name == "Supa de pui" ).ID,
DeliveryID = deliveries.Single(i => i.DeliveryName == "Tazz").ID
 },
 new DeliveryFood {
 FoodID = foods.Single(c => c.Name == "Cheesecake" ).ID,
DeliveryID = deliveries.Single(i => i.DeliveryName == "Glovo").ID
 },
 new DeliveryFood {
FoodID = foods.Single(c => c.Name == "Shaorma" ).ID,
 DeliveryID = deliveries.Single(i => i.DeliveryName == "Food Panda").ID
 },
             };
            foreach (DeliveryFood pb in deliveryfoods)
            {
                context.DeliveryFoods.Add(pb);
            }
            context.SaveChanges();
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using WebApplication_Oltean_Cristina.Models;
using Microsoft.EntityFrameworkCore;
using WebApplication_Oltean_Cristina.Data;
using WebApplication_Oltean_Cristina.Models.RestaurantViewModels;

namespace WebApplication_Oltean_Cristina.Controllers
{
    public class HomeController : Controller
    {
        private readonly RestaurantContext _context;
        public HomeController(RestaurantContext context)
        {
            _context = context;
        }
     

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Chat()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public async Task<ActionResult> Statistics()
        {
            IQueryable<OrderGroup> data =
            from order in _context.Orders
            group order by order.OrderDate into dateGroup
            select new OrderGroup()
            {
                OrderDate = dateGroup.Key,
                FoodCount = dateGroup.Count()
            };
            return View(await data.AsNoTracking().ToListAsync());
        }
    }
}

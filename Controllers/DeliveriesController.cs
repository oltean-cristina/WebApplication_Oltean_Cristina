using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_Oltean_Cristina.Data;
using WebApplication_Oltean_Cristina.Models;
using WebApplication_Oltean_Cristina.Models.RestaurantViewModels;


namespace WebApplication_Oltean_Cristina.Controllers
{

    public class DeliveriesController : Controller
    {
        private readonly RestaurantContext _context;

        public DeliveriesController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: Deliveries
        public async Task<IActionResult> Index(int? id, int? foodID)
        {
            var viewModel = new DeliveryIndexData
            {
                Deliveries = await _context.Deliveries

           .Include(i => i.DeliveryFoods)
            .ThenInclude(i => i.Food)
            .ThenInclude(i => i.Orders)
            .ThenInclude(i => i.Customer)
            .AsNoTracking()
            .OrderBy(i => i.DeliveryName)
            .ToListAsync()
            };
            if (id != null)
            {
                ViewData["DeliveryID"] = id.Value;
                Delivery delivery = viewModel.Deliveries.Where(
                i => i.ID == id.Value).Single();
                viewModel.Foods = delivery.DeliveryFoods.Select(s => s.Food);
            }
            if (foodID != null)
            {
                ViewData["FoodID"] = foodID.Value;
                viewModel.Orders = viewModel.Foods.Where(
                x => x.ID == foodID).Single().Orders;
            }
            return View(viewModel);
        }


        // GET: Deliveries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // GET: Deliveries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deliveries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,DeliveryName,Adress")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delivery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delivery);
        }

        // GET: Deliveries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var delivery = await _context.Deliveries
                .Include(i => i.DeliveryFoods).ThenInclude(i => i.Food)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (delivery == null)
            {
                return NotFound();
            }
            PopulateDeliveryFoodData(delivery);
            return View(delivery);

        }
        private void PopulateDeliveryFoodData(Delivery delivery)
        {
            var allFoods = _context.Foods;
            var deliveryFoods = new HashSet<int>(delivery.DeliveryFoods.Select(c => c.FoodID));
            var viewModel = new List<DeliveryFoodData>();
            foreach (var food in allFoods)
            {
                viewModel.Add(new DeliveryFoodData
                {
                    FoodID = food.ID,
                    Name = food.Name,
                    IsDelivery = deliveryFoods.Contains(food.ID)
                });
            }
            ViewData["Foods"] = viewModel;


        }

        // POST: Deliveries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, string[] selectedFoods)
        {
            if (id == null)
            {
                return NotFound();
            }
            var deliveryToUpdate = await _context.Deliveries
            .Include(i => i.DeliveryFoods)
            .ThenInclude(i => i.Food)
            .FirstOrDefaultAsync(m => m.ID == id);
            if (await TryUpdateModelAsync<Delivery>(
            deliveryToUpdate,
            "",
            i => i.DeliveryName, i => i.Adress))
            {
                UpdateDeliveryFoods(selectedFoods, deliveryToUpdate);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException /* ex */)
                {

                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, ");
                }
                return RedirectToAction(nameof(Index));
            }
            UpdateDeliveryFoods(selectedFoods, deliveryToUpdate);
            PopulateDeliveryFoodData(deliveryToUpdate);
            return View(deliveryToUpdate);
        }
        private void UpdateDeliveryFoods(string[] selectedFoods, Delivery deliveryToUpdate)
        {
            if (selectedFoods == null)
            {
                deliveryToUpdate.DeliveryFoods = new List<DeliveryFood>();
                return;
            }
            var selectedFoodsHS = new HashSet<string>(selectedFoods);
            var deliveryFoods = new HashSet<int>
            (deliveryToUpdate.DeliveryFoods.Select(c => c.Food.ID));
            foreach (var food in _context.Foods)
            {
                if (selectedFoodsHS.Contains(food.ID.ToString()))
                {
                    if (!deliveryFoods.Contains(food.ID))
                    {
                        deliveryToUpdate.DeliveryFoods.Add(new DeliveryFood
                        {
                            DeliveryID = deliveryToUpdate.ID,
                            FoodID = food.ID
                        });
                    }
                }
                else
                {
                    if (deliveryFoods.Contains(food.ID))
                    {
                        DeliveryFood foodToRemove = deliveryToUpdate.DeliveryFoods.FirstOrDefault(i
                       => i.FoodID == food.ID);
                        _context.Remove(foodToRemove);
                    }
                }
            }
        }

        // GET: Deliveries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Deliveries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryExists(int id)
        {
            return _context.Deliveries.Any(e => e.ID == id);
        }
    }
}

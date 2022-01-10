using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApplication_Oltean_Cristina.Data;
using WebApplication_Oltean_Cristina.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication_Oltean_Cristina.Controllers
{

    public class FoodsController : Controller
    {
        private readonly RestaurantContext _context;

        public FoodsController(RestaurantContext context)
        {
            _context = context;
        }

        // GET: Foods
        [AllowAnonymous]
        public async Task<IActionResult> Index(string sortOrder,
            string searchString,
            string currentFilter,
            int? pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["TitleSortParm"] = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            var foods = from b in _context.Foods
                        select b;
            if (!String.IsNullOrEmpty(searchString))
            {
                foods = foods.Where(s => s.Name.Contains(searchString));
            }
            foods = sortOrder switch
            {
                "title_desc" => foods.OrderByDescending(b => b.Name),
                "Price" => foods.OrderBy(b => b.Price),
                "price_desc" => foods.OrderByDescending(b => b.Price),
                _ => foods.OrderBy(b => b.Name),
            };
            int pageSize = 2;
            return View(await PaginatedList<Food>.CreateAsync(foods.AsNoTracking(), pageNumber ??
           1, pageSize));
            
        }

        // GET: Foods/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var food = await _context.Foods
            .Include(s => s.Orders)
            .ThenInclude(e => e.Customer)
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.ID == id);

            if (food == null)
            {
                return NotFound();
            }

            return View(food);
        }

        // GET: Foods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Foods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Category,Price")] Food food)
        {
            try
            {
                if (ModelState.IsValid)
            {
                _context.Add(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
                 }
            }
            catch (DbUpdateException /* ex*/)
            {

                ModelState.AddModelError("", "Unable to save changes. " +
                "Try again, and if the problem persists ");
            }
            return View(food);
        }

        // GET: Foods/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods.FindAsync(id);
            if (food == null)
            {
                return NotFound();
            }
            return View(food);
        }

        // POST: Foods/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var studentToUpdate = await _context.Foods.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Food>(studentToUpdate,"",s => s.Category, s => s.Name, s => s.Price))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException /* ex */)
                {
                    ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Foods/Delete/5
        public async Task<IActionResult> Delete(int? id, bool? saveChangesError = false)

        {
            if (id == null)
            {
                return NotFound();
            }

            var food = await _context.Foods
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (food == null)
            {
                return NotFound();
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewData["ErrorMessage"] =
                "Delete failed. Try again";
            }

            return View(food);
        }

        // POST: Foods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var food = await _context.Foods.FindAsync(id);

            if (food == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.Foods.Remove(food);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {

                return RedirectToAction(nameof(Delete), new { id, saveChangesError = true });
            }
        }

        private bool FoodExists(int id)
        {
            return _context.Foods.Any(e => e.ID == id);
        }
    }
}

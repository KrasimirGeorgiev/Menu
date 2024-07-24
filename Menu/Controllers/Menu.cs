using Microsoft.AspNetCore.Mvc;
using Menu.Data;
using Menu.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Menu.Controllers
{
    public class Menu : Controller
    {
        private readonly MenuContext _context;
        public Menu(MenuContext context) 
        {
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var dishes = from d in _context.Dishes
                         select d;

            if (!searchString.IsNullOrEmpty())
            {
                dishes = dishes.Where(d => d.Name.Contains(searchString));
            }

            return View(await dishes.ToListAsync());
        }

        public async Task<IActionResult> Details (int? id)
        {
            var dish = await _context.Dishes
                .Include(di => di.DishIngredients)
                .ThenInclude(i => i.Ingredient)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (dish == null)
            {
                return NotFound();
            }

            return View(dish);
        }
    }
}

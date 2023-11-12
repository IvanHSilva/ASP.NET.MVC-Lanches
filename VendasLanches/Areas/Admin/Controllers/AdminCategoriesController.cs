using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VendasLanches.Context;
using VendasLanches.Models;

namespace VendasLanches.Areas.Admin.Controllers {
    
    [Area("Admin")]
    public class AdminCategoriesController : Controller {
    
        private readonly AppDbContext _context;

        public AdminCategoriesController(AppDbContext context) {
            _context = context;
        }

        // GET: Admin/AdminCategories
        public async Task<IActionResult> Index() {
            return _context.Categories != null ?
                        View(await _context.Categories.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Categories'  is null.");
        }

        // GET: Admin/AdminCategories/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Categories == null) {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null) {
                return NotFound();
            }

            return View(category);
        }

        // GET: Admin/AdminCategories/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Admin/AdminCategories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,RegDate")] Category category) {
            int id = 1;
            Category categ = _context.Categories.OrderBy(c => c.Id).LastOrDefault()!;
            if (categ!= null) { id = categ.Id + 1; }

            category!.Id = id;

            if (ModelState.IsValid) {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/AdminCategories/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Categories == null) {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null) {
                return NotFound();
            }
            return View(category);
        }

        // POST: Admin/AdminCategories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,RegDate")] Category category) {
            if (id != category.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(category);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!CategoryExists(category.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Admin/AdminCategories/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Categories == null) {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null) {
                return NotFound();
            }

            return View(category);
        }

        // POST: Admin/AdminCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Categories == null) {
                return Problem("Entity set 'AppDbContext.Categories'  is null.");
            }
            var category = await _context.Categories.FindAsync(id);
            if (category != null) {
                _context.Categories.Remove(category);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CategoryExists(int id) {
            return (_context.Categories?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

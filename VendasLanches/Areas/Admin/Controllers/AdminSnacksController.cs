using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using VendasLanches.Context;
using VendasLanches.Models;

namespace VendasLanches.Areas.Admin.Controllers {
    [Area("Admin")]
    public class AdminSnacksController : Controller {
        private readonly AppDbContext _context;

        public AdminSnacksController(AppDbContext context) {
            _context = context;
        }

        // GET: Admin/AdminSnacks
        //public async Task<IActionResult> Index() {
        //    return _context.Snacks != null ?
        //                View(await _context.Snacks.ToListAsync()) :
        //                Problem("Entity set 'AppDbContext.Snacks'  is null.");
        //}

        public async Task<IActionResult> Index(string filter, int pageindex = 1,
            string sort = "Name") {

            IQueryable<Snack> result = _context.Snacks.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter)) {
                result = result.Where(s => s.Name.Contains(filter));
            }
            PagingList<Snack> model = await PagingList.CreateAsync(result, 5, pageindex,
                sort, "Name");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        // GET: Admin/AdminSnacks/Details/5
        public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Snacks == null) {
                return NotFound();
            }

            var snack = await _context.Snacks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (snack == null) {
                return NotFound();
            }

            return View(snack);
        }

        // GET: Admin/AdminSnacks/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Admin/AdminSnacks/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description,Details,Price,Image,Miniature,Favorite,HaveStock,Category,RegDate")] Snack snack) {
            
            int id = 1;
            Snack snc = _context.Snacks.OrderBy(s => s.Id).LastOrDefault()!;
            if (snc != null) { id = snc.Id + 1; }

            snack!.Id = id;

            if (ModelState.IsValid) {
                _context.Add(snack);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(snack);
        }

        // GET: Admin/AdminSnacks/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Snacks == null) {
                return NotFound();
            }

            var snack = await _context.Snacks.FindAsync(id);
            if (snack == null) {
                return NotFound();
            }
            ViewBag.Category = new SelectList(_context.Categories,"Id", "Name", snack.Category);
            return View(snack);
        }

        // POST: Admin/AdminSnacks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Description,Details,Price,Image,Miniature,Favorite,HaveStock,Category,RegDate")] Snack snack) {
            if (id != snack.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(snack);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!SnackExists(snack.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(snack);
        }

        // GET: Admin/AdminSnacks/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Snacks == null) {
                return NotFound();
            }

            var snack = await _context.Snacks
                .FirstOrDefaultAsync(m => m.Id == id);
            if (snack == null) {
                return NotFound();
            }

            return View(snack);
        }

        // POST: Admin/AdminSnacks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Snacks == null) {
                return Problem("Entity set 'AppDbContext.Snacks'  is null.");
            }
            var snack = await _context.Snacks.FindAsync(id);
            if (snack != null) {
                _context.Snacks.Remove(snack);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SnackExists(int id) {
            return (_context.Snacks?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

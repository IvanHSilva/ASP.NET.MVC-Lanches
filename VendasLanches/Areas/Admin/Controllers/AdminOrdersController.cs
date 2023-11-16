using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using VendasLanches.Context;
using VendasLanches.Models;

namespace VendasLanches.Areas.Admin.Controllers {
    [Area("Admin")]
    public class AdminOrdersController : Controller {
        private readonly AppDbContext _context;

        public AdminOrdersController(AppDbContext context) {
            _context = context;
        }

        // GET: Admin/AdminOrders
        //public async Task<IActionResult> Index() {
        //    return _context.Orders != null ?
        //                View(await _context.Orders.ToListAsync()) :
        //                Problem("Entity set 'AppDbContext.Orders'  is null.");
        //}

        public async Task<IActionResult> Index(string filter, int pageindex = 1,
            string sort = "Client") {
            
            IQueryable<Order> result = _context.Orders.AsNoTracking().AsQueryable();
            
            if (!string.IsNullOrWhiteSpace(filter)) {
                result = result.Where(o => o.Client.Contains(filter));
            }
            PagingList<Order> model = await PagingList.CreateAsync(result, 5, pageindex,
                sort, "Client");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            
            return View(model);
        }

            // GET: Admin/AdminOrders/Details/5
            public async Task<IActionResult> Details(int? id) {
            if (id == null || _context.Orders == null) {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null) {
                return NotFound();
            }

            return View(order);
        }

        // GET: Admin/AdminOrders/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Admin/AdminOrders/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Client,Phone,EMail,TotalOrder,Items,RegDate,ShippingDate,DeliveryDate")] Order order) {

            int id = 1;
            Order ord = _context.Orders.OrderBy(s => s.Id).LastOrDefault()!;
            if (ord != null) { id = ord.Id++; }

            order!.Id = id;

            if (ModelState.IsValid) {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Admin/AdminOrders/Edit/5
        public async Task<IActionResult> Edit(int? id) {
            if (id == null || _context.Orders == null) {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null) {
                return NotFound();
            }
            return View(order);
        }

        // POST: Admin/AdminOrders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Client,Phone,EMail,TotalOrder,Items,RegDate,ShippingDate,DeliveryDate")] Order order) {
            if (id != order.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!OrderExists(order.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Admin/AdminOrders/Delete/5
        public async Task<IActionResult> Delete(int? id) {
            if (id == null || _context.Orders == null) {
                return NotFound();
            }

            var order = await _context.Orders
                .FirstOrDefaultAsync(m => m.Id == id);
            if (order == null) {
                return NotFound();
            }

            return View(order);
        }

        // POST: Admin/AdminOrders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            if (_context.Orders == null) {
                return Problem("Entity set 'AppDbContext.Orders'  is null.");
            }
            var order = await _context.Orders.FindAsync(id);
            if (order != null) {
                _context.Orders.Remove(order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id) {
            return (_context.Orders?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}

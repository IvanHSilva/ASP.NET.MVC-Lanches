using Microsoft.EntityFrameworkCore;
using VendasLanches.Context;
using VendasLanches.Models;

namespace VendasLanches.Areas.Admin.Services; 

public class SellersReportService {

    private readonly AppDbContext _context;

    public SellersReportService(AppDbContext context) {
        _context = context;
    }

    public async Task<List<Order>> FindByDateAsync(DateTime? minDate, DateTime? maxDate) {

        IQueryable<Order> result = from obj in _context.Orders select obj;

        if (minDate.HasValue) {
            result = result.Where(o => o.DeliveryDate >= minDate.Value);
        }
        if (maxDate.HasValue) {
            result = result.Where(o => o.DeliveryDate <= maxDate.Value);
        }

        return await result.Include(i => i.OrderItems).
            ThenInclude(s => s.Snack).
            OrderByDescending(o => o.DeliveryDate).
            ToListAsync();
    }
}

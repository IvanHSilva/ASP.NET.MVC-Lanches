using Microsoft.AspNetCore.Mvc;
using VendasLanches.Areas.Admin.Services;
using VendasLanches.Models;

namespace VendasLanches.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminSellersReportController : Controller {

    private readonly SellersReportService _service;

    public AdminSellersReportController(SellersReportService service) {
        _service = service;
    }

    public ActionResult Index() {
        return View();
    }

    public async Task<IActionResult> SimpleSellersReport(DateTime? minDate,
        DateTime? maxDate) {

        if (!minDate.HasValue) {
            minDate = new DateTime(DateTime.Now.Year, 1, 1);
        }
        if (!maxDate.HasValue) {
            maxDate = DateTime.Now;
        }

        ViewData["minDate"] = minDate.Value.ToString("yyyy-MM-dd");
        ViewData["maxDate"] = maxDate.Value.ToString("yyyy-MM-dd");

        List<Order> result = await _service.FindByDateAsync(minDate, maxDate);

        return View(result);
    }
}

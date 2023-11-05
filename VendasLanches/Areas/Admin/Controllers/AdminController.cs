using Microsoft.AspNetCore.Mvc;

namespace VendasLanches.Areas.Admin.Controllers;

[Area("Admin")]
public class AdminController : Controller {

    public IActionResult Index() {
        return View();
    }
}

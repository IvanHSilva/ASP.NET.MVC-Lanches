using Microsoft.AspNetCore.Mvc;

namespace VendasLanches.Controllers;

public class ContactController : Controller {
    
    public IActionResult Index() {
        return View();
    }
}

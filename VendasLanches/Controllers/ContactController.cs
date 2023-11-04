using Microsoft.AspNetCore.Mvc;

namespace VendasLanches.Controllers;

public class ContactController : Controller {
    
    public IActionResult Index() {
        //if (User.Identity!.IsAuthenticated) return View(); 
        //else return RedirectToAction("Login", "Account");
        return View();
    }
}

using Microsoft.AspNetCore.Mvc;
using VendasLanches.Models;
using VendasLanches.Repositories.Interfaces;

namespace VendasLanches.Controllers; 

public class CartController : Controller {

    private readonly ISnackRepository _snackRepository = null!;
    private readonly Cart _cart = null!;

    public CartController(ISnackRepository snackRepository, Cart cart) {
        _snackRepository = snackRepository;
        _cart = cart;
    }

    public IActionResult Index() {
        return View();
    }
}

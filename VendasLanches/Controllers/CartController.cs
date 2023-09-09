using Microsoft.AspNetCore.Mvc;
using VendasLanches.Models;
using VendasLanches.Repositories.Interfaces;
using VendasLanches.ViewModels;

namespace VendasLanches.Controllers; 

public class CartController : Controller {

    private readonly ISnackRepository _snackRepository = null!;
    private readonly Cart _cart = null!;

    public CartController(ISnackRepository snackRepository, Cart cart) {
        _snackRepository = snackRepository;
        _cart = cart;
    }

    public IActionResult Index() {

        List<CartItem> itens = _cart.GetCartItems();
        _cart.CartItems = itens;

        CartViewModel cartVM = new CartViewModel {
            Cart = _cart,
            CartTotal = _cart.GetCartTotal(),
        };

        return View(cartVM);
    }

    public IActionResult AddItemToCart(int snackId) {

        var selectedSnack = _snackRepository.Snacks.FirstOrDefault(
            s => s.Id == snackId);
        if (selectedSnack != null) {
            _cart.AddToCart(selectedSnack);
        }

        return RedirectToAction("Index");
    }

    public IActionResult RemoveItemToCart(int snackId) {

        var selectedSnack = _snackRepository.Snacks.FirstOrDefault(
            s => s.Id == snackId);
        if (selectedSnack != null) {
            _cart.RemoveFromCart(selectedSnack);
        }

        return RedirectToAction("Index");
    }
}

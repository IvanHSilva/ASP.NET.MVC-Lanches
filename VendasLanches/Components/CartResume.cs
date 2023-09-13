using Microsoft.AspNetCore.Mvc;
using VendasLanches.Models;
using VendasLanches.ViewModels;

namespace VendasLanches.Components; 

public class CartResume : ViewComponent {

    private readonly Cart _cart;

    public CartResume(Cart cart) {
        _cart = cart;
    }

    public IViewComponentResult Invoke() {

        List<CartItem> itens = _cart.GetCartItems();
        //List<CartItem> itens = new List<CartItem> {
        //    new CartItem(),
        //    new CartItem(),
        //};
        _cart.CartItems = itens;

        CartViewModel cartVM = new CartViewModel {
            Cart = _cart,
            CartTotal = _cart.GetCartTotal(),
        };

        return View(cartVM);
    }

}

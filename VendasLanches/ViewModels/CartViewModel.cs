using VendasLanches.Models;

namespace VendasLanches.ViewModels; 

public class CartViewModel {
    public Cart Cart { get; set; } = null!;
    public double CartTotal { get; set; }

    public CartViewModel(){}

    public CartViewModel(Cart cart, double cartTotal) {
        Cart = cart;
        CartTotal = cartTotal;
    }
}

using Microsoft.AspNetCore.Mvc;
using VendasLanches.Models;
using VendasLanches.Repositories.Interfaces;

namespace VendasLanches.Controllers; 

public class OrderController : Controller {

    private readonly IOrderRepository _orderRepository;
    private readonly Cart _cart;

    public OrderController(IOrderRepository orderRepository, Cart cart) {
        _orderRepository = orderRepository;
        _cart = cart;
    }

    [HttpGet]
    public IActionResult Checkout() {
        return View();
    }

    [HttpPost]
    public IActionResult Checkout(Order order) {
        
        int totalItems = 0;
        double totalAmount = 0;

        // Get cart items
        List<CartItem> items = _cart.GetCartItems();
        _cart.CartItems = items;

        // Check if there are items on cart
        if (_cart.CartItems.Count == 0) {
            ModelState.AddModelError("", "Seu carrinho está vazio!!!!");
        }

        // Calculate amount of the cart
        foreach(CartItem item in items) {
            totalItems += item.Quantity;
            totalAmount += (item.Snack.Price * item.Quantity);
        }

        // Populate order with cart data
        order.Items = totalItems;
        order.TotalOrder = totalAmount;

        // Validate data
        if (ModelState.IsValid) {
            // create Order
            _orderRepository.CreateOrder(order);

            // define messages
            ViewBag.CheckoutMessage = "Obrigado por comprar conosco! :-)";
            ViewBag.TotalAmount = _cart.GetCartTotal();

            // clear the cart
            _cart.ClearCart();

            // return View
            return View("~/Views/Order/Order.cshtml", order);
        }

        // return View
        return View(order);
    }
}

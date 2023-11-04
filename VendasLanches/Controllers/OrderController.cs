using Microsoft.AspNetCore.Authorization;
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

    [Authorize]
    [HttpGet]
    public IActionResult Checkout() {
        return View();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Checkout(Order order) {
        
        int totalItems = 0;
        double totalAmount = 0.0;

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
            
            //// Create Order
            _orderRepository.CreateOrder(order);

            // Define messages
            ViewBag.Message = "Obrigado por comprar conosco! :-)";
            ViewBag.Amount = _cart.GetCartTotal();

            // Clear the cart
            _cart.ClearCart();

            // Return View
            return View("~/Views/Order/Confirm.cshtml", order);
        //} else {
        //    return Json(new { success = false, errors = ModelState.Values.Where(i => i.Errors.Count > 0) });
        }

        // Return View
        return View(order);
    }
}

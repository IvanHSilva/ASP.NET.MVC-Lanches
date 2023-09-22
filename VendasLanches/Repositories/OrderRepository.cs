using VendasLanches.Context;
using VendasLanches.Models;
using VendasLanches.Repositories.Interfaces;

namespace VendasLanches.Repositories;

public class OrderRepository : IOrderRepository {
    
    private readonly AppDbContext _context;
    private readonly Cart _cart;

    public OrderRepository(AppDbContext context, Cart cart) {
        _context = context;
        _cart = cart;
    }

    public void CreateOrder(Order order) {

        int id = 1;
        Order ord = _context.Orders.OrderBy(c => c.Id).LastOrDefault()!;
        if (ord != null) { id = ord.Id + 1; }

        order.Id = id;
        order.RegDate = DateTime.Now;
        order.ShippingDate = DateTime.Now;

        IEnumerable<CartItem> cartItems = _cart.GetCartItems();
        foreach (CartItem item in cartItems) {
            OrderItem orderItem = new OrderItem() {
                Id = id,
                SnackId = item.SnackId,
                SnackName = item.SnackName,
                Quantity = item.Quantity,
                Price = item.Snack.Price,
                Total = item.Quantity * item.Snack.Price,
                CartId = item.CartId,
                OrderId = id,
            };
            _context.OrderItems.Add(orderItem);
        }
        _context.SaveChanges();
    }
}

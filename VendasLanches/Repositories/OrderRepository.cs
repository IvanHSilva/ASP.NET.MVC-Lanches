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
        Order ord = _context.Orders.OrderBy(o => o.Id).LastOrDefault()!;
        if (ord != null) { id = ord.Id + 1; }

        order.Id = id;
        order.RegDate = DateTime.Now;
        order.ShippingDate = DateTime.Now;
        order.DeliveryDate = DateTime.Now;
        //order.Client = string.Empty;
        //order.Phone = string.Empty;
        //order.EMail = string.Empty;
        order.Items = _cart.CartItems.Count;
        order.TotalOrder = _cart.GetCartTotal();
        _context.Orders.Add(order);
        _context.SaveChanges();

        int itemId = 0;
        OrderItem ordItem = _context.OrderItems.OrderBy(i => i.Id).LastOrDefault()!;
        if (ordItem != null) { itemId = ordItem.Id; }

        IEnumerable<CartItem> cartItems = _cart.GetCartItems();
        foreach (CartItem item in cartItems) {
            itemId++;
            OrderItem orderItem = new OrderItem() {
                Id = itemId,
                SnackId = item.SnackId,
                SnackName = item.SnackName,
                Quantity = item.Quantity,
                Price = item.Snack.Price,
                Total = item.Quantity * item.Snack.Price,
                CartId = _cart.CartId,
                OrderId = id,
                RegDate = DateTime.Now,
            };
            _context.OrderItems.Add(orderItem);
            //order.OrderItems!.Add(orderItem);
        }
        _context.SaveChanges();
    }
}

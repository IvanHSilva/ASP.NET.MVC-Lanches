using Microsoft.EntityFrameworkCore;
using VendasLanches.Context;

namespace VendasLanches.Models;

public class Cart {

    // DBContext Instance
    private readonly AppDbContext _context;

    public Cart(AppDbContext context) {
        _context = context;
    }

    // Attributes
    public string CartId { get; set; } = string.Empty;
    public List<CartItem> CartItems { get; set; } = null!;

    // Methods
    public static Cart GetCart(IServiceProvider service) {

        // Define a session
        ISession? session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext!.Session;

        // Obtain a service of the context
        AppDbContext context = service.GetService<AppDbContext>()!;

        // Obtain or generate CartId
        string cartId = session!.GetString("CartId") ?? Guid.NewGuid().ToString();

        // Set CartId in session
        session!.SetString("CartId", cartId);

        return new Cart(context!) { CartId = cartId };
    }

    public void AddToCart(Snack snack) {

        int id = 1; 
        CartItem cartItem = _context.CartItems.OrderBy(c => c.Id).LastOrDefault()!;
        if (cartItem != null) { id = cartItem.Id + 1; }

        cartItem = _context.CartItems.SingleOrDefault(
        s => s.SnackId == snack.Id && s.CartId == CartId)!;


        if (cartItem == null) {
            cartItem = new CartItem {
                Id = id,
                CartId = CartId,
                SnackId = snack.Id,
                SnackName = snack.Name,
                Quantity = 1,
                RegDate = DateTime.Now
            };
            _context.CartItems.Add(cartItem);
        } else {
            cartItem.Quantity++;
        }
        _context.SaveChanges();
    }

    public int RemoveFromCart(Snack snack) {

        CartItem cartItem = _context.CartItems.SingleOrDefault(
            s => s.SnackId == snack.Id && s.CartId == CartId)!;

        int quantity = 0;

        if (cartItem != null) {
            if (cartItem.Quantity > 1) {
                cartItem.Quantity--;
                quantity = cartItem.Quantity;
            } else {
                _context.CartItems.Remove(cartItem);
            }
        }
        _context.SaveChanges();
        return quantity;
    }

    public List<CartItem> GetCartItems() {

        return CartItems ?? (CartItems = _context.CartItems
            .Where(c => c.CartId == CartId).Include(s => s.Snack).ToList());
    }

    public void ClearCart() {

        IQueryable<CartItem> cartItems = _context.CartItems
            .Where(c => c.CartId == CartId);
        _context.CartItems.RemoveRange(cartItems);
        _context.SaveChanges();
    }

    public double GetCartTotal() {
        double totalCart = _context.CartItems
            .Where(c => c.CartId == CartId)
            .Select(c => c.Snack.Price * c.Quantity).Sum();
        return totalCart;
    }
}

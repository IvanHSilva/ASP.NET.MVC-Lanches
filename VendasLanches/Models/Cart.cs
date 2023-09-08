using Microsoft.AspNetCore.Http;
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
        ISession? session = service.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

        // Obtain a service of the context
        AppDbContext context = service.GetService<AppDbContext>();

        // Obtain or generate CartId
        string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();

        // Set CartId in session
        session.SetString("CartId", cartId);

        return new Cart(context) {CartId = cartId};
    }
}

using Microsoft.EntityFrameworkCore;
using VendasLanches.Models;

namespace VendasLanches.Context; 

public class AppDbContext : DbContext {

    public AppDbContext(DbContextOptions<AppDbContext> options) : 
        base(options) {}

    public DbSet<Category> Categories { get; set; } = null!;
    public DbSet<Snack> Snacks { get; set; } = null!;
    public DbSet<CartItem> CartItems { get; set; } = null!;
    public DbSet<Order> Orders { get; set; } = null!;
    public DbSet<OrderItem> OrderItems { get; set; } = null!;
}

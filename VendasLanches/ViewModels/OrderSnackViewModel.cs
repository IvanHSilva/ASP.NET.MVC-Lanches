using VendasLanches.Models;

namespace VendasLanches.ViewModels; 

public class OrderSnackViewModel {
    
    public Order Order { get; set; } = null!;
    public IEnumerable<OrderItem> OrderItems { get; set; } = null!;
}

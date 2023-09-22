using VendasLanches.Models;

namespace VendasLanches.Repositories.Interfaces; 

public interface IOrderRepository {
    void CreateOrder(Order order);
}

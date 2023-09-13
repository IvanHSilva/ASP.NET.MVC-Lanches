using VendasLanches.Models;

namespace VendasLanches.Repositories.Interfaces; 

public interface ISnackRepository {
    
    IEnumerable<Snack> Snacks { get; }
    IEnumerable<Snack>? FavoriteSnacks { get; }
    Snack GetSnackById(int id);
}

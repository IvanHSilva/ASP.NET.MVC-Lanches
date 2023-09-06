using VendasLanches.Models;

namespace VendasLanches.Repositories.Interfaces; 

public interface ICategoryRepository {
    IEnumerable<Category> Categories { get; }
}

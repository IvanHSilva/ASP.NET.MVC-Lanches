using VendasLanches.Models;

namespace VendasLanches.ViewModels; 

public class HomeViewModel {

    public IEnumerable<Snack> FavoriteSnacks { get; set; } = null!;
}

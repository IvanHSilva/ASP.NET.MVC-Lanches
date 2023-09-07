using VendasLanches.Models;

namespace VendasLanches.ViewModels; 

public class SnackListViewModel {
    public IEnumerable<Snack> Snacks { get; set; } = null!;
}

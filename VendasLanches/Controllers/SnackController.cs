using Microsoft.AspNetCore.Mvc;
using VendasLanches.Models;
using VendasLanches.Repositories.Interfaces;

namespace VendasLanches.Controllers; 

public class SnackController : Controller {

    private readonly ISnackRepository _snackRepository;

    public SnackController(ISnackRepository snackRepository) {
        _snackRepository = snackRepository;
    }

    public IActionResult List() {

        IEnumerable<Snack> snacks = _snackRepository.Snacks;
        return View(snacks);
    }
}

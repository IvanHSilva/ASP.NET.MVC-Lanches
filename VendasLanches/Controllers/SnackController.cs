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
        
        ViewData["Title"] = "Todos os Lanches";
        ViewData["Date"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

        IEnumerable<Snack> snacks = _snackRepository.Snacks;
        
        int totalSnacks = snacks.Count();
        ViewBag.TotalText = "Total de lanches: ";
        ViewBag.TotalSnacks = totalSnacks;

        return View(snacks);
    }
}

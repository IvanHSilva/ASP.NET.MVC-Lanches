using Microsoft.AspNetCore.Mvc;
using VendasLanches.Models;
using VendasLanches.Repositories.Interfaces;
using VendasLanches.ViewModels;

namespace VendasLanches.Controllers; 

public class SnackController : Controller {

    private readonly ISnackRepository _snackRepository;

    public SnackController(ISnackRepository snackRepository) {
        _snackRepository = snackRepository;
    }

    public IActionResult List() {

        ViewData["Title"] = "Todos os Lanches";
        ViewData["Date"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm");

        // IEnumerable<Snack> snacks = _snackRepository.Snacks;
        SnackListViewModel snackLVM = new SnackListViewModel();
        snackLVM.Snacks = _snackRepository.Snacks;

        int totalSnacks = snackLVM.Snacks.Count();
        ViewBag.TotalText = "Total de lanches: ";
        ViewBag.TotalSnacks = totalSnacks;

        return View(snackLVM);
    }
}

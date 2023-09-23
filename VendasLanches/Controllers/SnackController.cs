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

    public IActionResult List(string category) {

        IEnumerable<Snack> snacks = null!;
        string snackCategory = string.Empty;

        if (string.IsNullOrEmpty(category)) {
            snacks = _snackRepository.Snacks.OrderBy(s => s.Id);
            snackCategory = "Todos os lanches";
        } else {
            snacks = _snackRepository.Snacks.Where(s => s.Category == category)
                .OrderBy(s => s.Name);
            snackCategory = category;
        }

        ViewData["Title"] = snackCategory;
        ViewData["Date"] = DateTime.Now;

        int totalSnacks = snacks.Count();
        ViewBag.Text = "Total de Lanches:";
        ViewBag.Total = totalSnacks;

        SnackListViewModel snackLVM = new SnackListViewModel {
            Snacks = snacks,
            Category = snackCategory
        };

        return View(snackLVM);
    }

    public IActionResult Details(int snackId) {

        Snack snack = _snackRepository.Snacks.FirstOrDefault(s => s.Id == snackId)!;
        return View(snack);
    }

    public ViewResult Search(string searchString) {

        IEnumerable<Snack> snacks;
        string category = string.Empty;

        if (string.IsNullOrEmpty(searchString)) {
            snacks = _snackRepository.Snacks.OrderBy(s => s.Id);
            category = "Todos os Lanches";
        } else {
            snacks = _snackRepository.Snacks
                .Where(s => s.Name.ToLower().Contains(searchString.ToLower()));
            if (snacks.Any()) category = "Lanches"; 
            else category = "Nenhum Lanche Encontrado";
        }

        return View("~/Views/Snack/List.cshtml", new SnackListViewModel {
            Category = category,
            Snacks = snacks
        });
    }
}

using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using VendasLanches.Models;
using VendasLanches.Repositories.Interfaces;
using VendasLanches.ViewModels;

namespace VendasLanches.Controllers {
    public class HomeController : Controller {

        private readonly ISnackRepository _snackRepository = null!;

        public HomeController(ISnackRepository snackRepository) {
            _snackRepository = snackRepository;
        }

        public IActionResult Index() {
            
            HomeViewModel homeVM = new HomeViewModel {
                FavoriteSnacks = _snackRepository.FavoriteSnacks!
            };

            return View(homeVM);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using VendasLanches.Models;
using VendasLanches.Repositories.Interfaces;

namespace VendasLanches.Components; 

public class CategoryMenu : ViewComponent {
    
    private readonly ICategoryRepository _categoryRepository;

    public CategoryMenu(ICategoryRepository categoryRepository) {
        _categoryRepository = categoryRepository;
    }

    public IViewComponentResult Invoke() {

        IEnumerable<Category> categories = _categoryRepository.Categories.OrderBy(c => c.Name);
        return View(categories);
    }
}

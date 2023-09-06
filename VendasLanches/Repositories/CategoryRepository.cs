using VendasLanches.Context;
using VendasLanches.Models;
using VendasLanches.Repositories.Interfaces;

namespace VendasLanches.Repositories;

public class CategoryRepository : ICategoryRepository {

    private readonly AppDbContext _context;

    public CategoryRepository(AppDbContext context) {
        _context = context;
    }

    public IEnumerable<Category> Categories => _context.Categories;
}

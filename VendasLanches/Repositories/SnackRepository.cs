﻿using Microsoft.EntityFrameworkCore;
using VendasLanches.Context;
using VendasLanches.Models;
using VendasLanches.Repositories.Interfaces;

namespace VendasLanches.Repositories; 

public class SnackRepository : ISnackRepository {
    
    private readonly AppDbContext _context;

    public SnackRepository(AppDbContext context) {
        _context = context;
    }

    public IEnumerable<Snack> Snacks => _context.Snacks.Include(c => c.Category);

    public IEnumerable<Snack> FavoriteSnacks => _context.Snacks.
        Where(s => s.Favorite).Include(c => c.Category);

    public Snack GetSnackById(int id) => _context.Snacks.FirstOrDefault(s => s.Id == id);
}
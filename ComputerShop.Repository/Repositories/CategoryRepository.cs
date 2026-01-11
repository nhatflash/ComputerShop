using ComputerShop.Repository.Context;
using ComputerShop.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Repository.Repositories;

public class CategoryRepository
{

    private readonly ApplicationDbContext _context; 

    public CategoryRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task AddCategoryAsync(Category category)
    {
        await _context.Categories.AddAsync(category);
        await _context.SaveChangesAsync();
    }


    public async Task<Category?> FindCategoryByIdAsync(Guid id)
    {
        return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Category?> FindCategoryByIdWithTrackingAsync(Guid id)
    {
        return await _context.Categories.AsTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> CategoryExistByNameAsync(string name)
    {
        return await _context.Categories.AnyAsync(c => c.Name == name);
    }

    public async Task<bool> CategoryExistByIdAsync(Guid id)
    {
        return await _context.Categories.AnyAsync(c => c.Id == id);
    }
}

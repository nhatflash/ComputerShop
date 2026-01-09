using System;
using ComputerShop.Repository.Context;
using ComputerShop.Repository.Models;

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
}

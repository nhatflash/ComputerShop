using ComputerShop.Repository.Context;
using ComputerShop.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Repository.Repositories;

public class ProductRepository
{

    private readonly ApplicationDbContext _context;

    public ProductRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddProductAsync(Product product)
    {
        await _context.Products.AddAsync(product);
        await _context.SaveChangesAsync();
    }

    public async Task<Product?> FindProductByIdAsync(Guid id)
    {
        return await _context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }


    public async Task<Product?> FindProductByIdWithTrackingAsync(Guid id)
    {
        return await _context.Products.AsTracking().FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product?> FindProductByIdAndImagesAsync(Guid id)
    {
        return await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product?> FindProductByIdAndImagesWithTrackingAsync(Guid id)
    {
        return await _context.Products.Include(p => p.ProductImages).AsTracking().FirstOrDefaultAsync(p => p.Id == id);
    }


    public async Task<bool> ProductExistByNameAsync(string name)
    {
        return await _context.Products.AnyAsync(p => p.Name == name);
    }


    public async Task<List<Product>> FindAllProductsAsync()
    {
        return await _context.Products.ToListAsync();
    }

    public async Task<List<Product>> FindAllProductsWithImagesAsync()
    {
        return await _context.Products.Include(p => p.ProductImages).ToListAsync();
    }
}

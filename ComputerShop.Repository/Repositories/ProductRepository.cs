using ComputerShop.Repository.Context;
using ComputerShop.Repository.Enums;
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

    public async Task<Product?> FindProductByIdIncludeImagesAsync(Guid id)
    {
        return await _context.Products.Include(p => p.ProductImages).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Product?> FindProductByIdIncludeImagesWithTrackingAsync(Guid id)
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

    public async Task<List<Product>> FindAllProductsIncludeImagesAsync()
    {
        return await _context.Products.Include(p => p.ProductImages).ToListAsync();
    }


    public async Task RestockProductsFromOrders(DateTimeOffset expirationTime)
    {
        await _context.Products
                    .Where(p => _context.OrderItems
                        .Any(oi => oi.ProductId == p.Id && oi.Order.Status == OrderStatus.Pending && oi.Order.OrderDate.CompareTo(expirationTime) <= 0))
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(p => p.StockQuantity, p => p.StockQuantity + _context.OrderItems
                            .Where(oi => oi.ProductId == p.Id && oi.Order.Status == OrderStatus.Pending && oi.Order.OrderDate.CompareTo(expirationTime) <= 0)
                            .Sum(oi => oi.Quantity)));
    }
}

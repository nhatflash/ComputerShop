using System;
using ComputerShop.Repository.Context;
using ComputerShop.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Repository.Repositories;

public class ProductImageRepository
{
    private readonly ApplicationDbContext _context;

    public ProductImageRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddProductImageAsync(ProductImage productImage)
    {
        await _context.ProductImages.AddAsync(productImage);
        await _context.SaveChangesAsync();
    }

    public async Task<ProductImage?> FindProductImageByIdAsync(long id)
    {
        return await _context.ProductImages.FirstOrDefaultAsync(pi => pi.Id == id);
    }

    public async Task<ProductImage?> FindProductImageByIdWithTrackingAsync(long id)
    {
        return await _context.ProductImages.AsTracking().FirstOrDefaultAsync(pi => pi.Id == id);
    }

    public async Task<List<ProductImage>> FindProductImagesByProductIdAsync(Guid productId)
    {
        return await _context.ProductImages.Where(pi => pi.ProductId == productId).ToListAsync();
    }
    
}

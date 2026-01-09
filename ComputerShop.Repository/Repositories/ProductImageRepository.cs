using System;
using ComputerShop.Repository.Context;
using ComputerShop.Repository.Models;

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


    
}

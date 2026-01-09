using System;
using ComputerShop.Repository.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace ComputerShop.Repository.Repositories;

public class UnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UserRepository UserRepository { get; }
    public ManufacturerRepository ManufacturerRepository { get; }
    public CategoryRepository CategoryRepository { get; }
    public ProductRepository ProductRepository { get; }
    public ProductImageRepository ProductImageRepository { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        UserRepository = new UserRepository(context);
        ManufacturerRepository = new ManufacturerRepository(context);
        CategoryRepository = new CategoryRepository(context);
        ProductRepository = new ProductRepository(context);
        ProductImageRepository = new ProductImageRepository(context);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

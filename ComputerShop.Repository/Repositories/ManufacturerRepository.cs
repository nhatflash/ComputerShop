using ComputerShop.Repository.Context;
using ComputerShop.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Repository.Repositories;

public class ManufacturerRepository
{

    private readonly ApplicationDbContext _context;

    public ManufacturerRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Manufacturer?> FindManufacturerByIdAsync(Guid id)
    {
        return await _context.Manufacturers.FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task<Manufacturer?> FindManufacturerByIdWithTrackingAsync(Guid id)
    {
        return await _context.Manufacturers.AsTracking().FirstOrDefaultAsync(m => m.Id == id);
    }
    
    public async Task<Manufacturer?> FindManufacturerByNameAsync(string name)
    {
        return await _context.Manufacturers.FirstOrDefaultAsync(m => m.Name == name);
    }

    public async Task<Manufacturer?> FindManufacturerByNameWithTrackingAsync(string name)
    {
        return await _context.Manufacturers.AsTracking().FirstOrDefaultAsync(m => m.Name == name);
    }


    public async Task<bool> ManufacturerExistByNameAsync(string name)
    {
        return await _context.Manufacturers.AnyAsync(m => m.Name == name);
    }


    public async Task AddManufacturerAsync(Manufacturer manufacturer)
    {
        await _context.Manufacturers.AddAsync(manufacturer);
        await _context.SaveChangesAsync();
    }


    public async Task<bool> ManufacturerExitByIdAsync(Guid id)
    {
        return await _context.Manufacturers.AnyAsync(m => m.Id == id);
    }

    
}

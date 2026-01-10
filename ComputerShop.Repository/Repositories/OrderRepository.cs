using System;
using ComputerShop.Repository.Context;
using ComputerShop.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Repository.Repositories;

public class OrderRepository
{

    private readonly ApplicationDbContext _context;

    public OrderRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task CreateOrderAsync(Order order)
    {
        await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order?> FindOrderByIdAsync(Guid id)
    {
        return await _context.Orders.FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order?> FindOrderByIdWithTrackingAsync(Guid id)
    {
        return await _context.Orders.AsTracking().FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order?> FindOrderByIdAndOrderItemsAsync(Guid id)
    {
        return await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order?> FindOrderByIdAndOrderItemsWithTrackingAsync(Guid id)
    {
        return await _context.Orders.Include(o => o.OrderItems).AsTracking().FirstOrDefaultAsync(o => o.Id == id);
    }
}

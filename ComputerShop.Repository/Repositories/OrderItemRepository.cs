using System;
using ComputerShop.Repository.Context;
using ComputerShop.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Repository.Repositories;

public class OrderItemRepository
{

    private readonly ApplicationDbContext _context;

    public OrderItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<OrderItem>> FindOrderItemByOrderIdAsync(Guid orderId)
    {
        return await _context.OrderItems.Where(o => o.OrderId == orderId).ToListAsync();
    }

    
}

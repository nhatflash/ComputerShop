using System;
using ComputerShop.Repository.Context;

namespace ComputerShop.Repository.Repositories;

public class CartItemRepository
{
    private readonly ApplicationDbContext _context; 

    public CartItemRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}

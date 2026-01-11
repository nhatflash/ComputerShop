using System;
using ComputerShop.Repository.Context;

namespace ComputerShop.Repository.Repositories;

public class CartRepository
{
    private readonly ApplicationDbContext _context;

    public CartRepository(ApplicationDbContext context)
    {
        _context = context;
    }
}

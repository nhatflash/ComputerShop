using System;
using ComputerShop.Repository.Context;
using Microsoft.EntityFrameworkCore.Storage;

namespace ComputerShop.Repository.Repositories;

public class UnitOfWork
{
    private readonly ApplicationDbContext _context;

    public UserRepository UserRepository { get; }

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
        UserRepository = new UserRepository(context);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}

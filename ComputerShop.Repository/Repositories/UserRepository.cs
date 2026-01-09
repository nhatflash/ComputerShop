using System;
using ComputerShop.Repository.Context;
using ComputerShop.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Repository.Repositories;

public class UserRepository
{

    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<User?> FindUserByIdAsync(Guid id)
    {
        return await _context.Users
        .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> FindUserByEmailAsync(string email)
    {
        return await _context.Users
        .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> FindUserByPhoneAsync(string phone)
    {
        return await _context.Users
        .FirstOrDefaultAsync(u => u.PhoneNumber == phone);
    }

    public async Task<User?> FindUserByIdWithTrackingAsync(Guid id)
    {
        return await _context.Users
        .AsTracking()
        .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User?> FindUserByEmailWithTrackingAsync(string email)
    {
        return await _context.Users
        .AsTracking()
        .FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> FindUserByPhoneWithTrackingAsync(string phone)
    {
        return await _context.Users
        .AsTracking()
        .FirstOrDefaultAsync(u => u.PhoneNumber == phone);
    }

    public async Task<bool> UserExistByEmail(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }


    public async Task<bool> UserExistByPhone(string phone)
    {
        return await _context.Users.AnyAsync(u => u.PhoneNumber == phone);
    }


    public async Task AddUserAsync(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
}

using ComputerShop.Repository.Context;
using ComputerShop.Repository.Models;
using Microsoft.EntityFrameworkCore;

namespace ComputerShop.Repository.Repositories;

public class PaymentRepository
{
    private readonly ApplicationDbContext _context; 

    public PaymentRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<Payment?> FindPaymentByIdAsync(Guid id)
    {
        return await _context.Payments.FirstOrDefaultAsync(p => p.Id == id);
    }


    public async Task<Payment?> FindPaymentByIdWithTrackingAsync(Guid id)
    {
        return await _context.Payments.AsTracking().FirstOrDefaultAsync(p => p.Id == id);
    }


    public async Task<Payment?> FindPaymentByIdIncludeOrderAsync(Guid id)
    {
        return await _context.Payments.Include(p => p.Order).FirstOrDefaultAsync(p => p.Id == id);
    }


    public async Task<Payment?> FindPaymentByIdIncludeOrderWithTrackingAsync(Guid id)
    {
        return await _context.Payments.Include(p => p.Order).AsTracking().FirstOrDefaultAsync(p => p.Id == id);
    }


    public async Task<List<Payment>> FindPaymentsByOrderIdAsync(Guid orderId)
    {
        return await _context.Payments.Where(p => p.OrderId == orderId).ToListAsync();
    }

    public async Task<List<Payment>> FindPaymentsByOrderIdWithTrackingAsync(Guid orderId)
    {
        return await _context.Payments.AsTracking().Where(p => p.OrderId == orderId).ToListAsync();
    }


    public async Task CreatePaymentAsync(Payment payment)
    {
        await _context.Payments.AddAsync(payment);
        await _context.SaveChangesAsync();
    }
}

using System;
using ComputerShop.Repository.Context;
using ComputerShop.Repository.Enums;
using ComputerShop.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ComputerShop.Service.Background;

public class CleanUnpaidOrdersTask : BackgroundService
{
    private readonly ILogger<CleanUnpaidOrdersTask> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public CleanUnpaidOrdersTask(ILogger<CleanUnpaidOrdersTask> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Clean unpaid orders task is starting...");

        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Clean unpaid orders task is executing...");
            using var scope = _serviceScopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var strategy = context.Database.CreateExecutionStrategy();
            await strategy.ExecuteAsync(async () =>
            {
                using var transaction = await context.Database.BeginTransactionAsync(stoppingToken);

                try
                {
                    _logger.LogInformation("Clean unpaid orders task begin processing...");
                    var expirationTime = DateTimeOffset.UtcNow.AddMinutes(-15);
                    await context.Products
                        .Where(p => context.OrderItems
                            .Any(oi => oi.ProductId == p.Id && oi.Order.Status == OrderStatus.Pending && oi.Order.OrderDate.CompareTo(expirationTime) <= 0))
                        .ExecuteUpdateAsync(setters => setters
                            .SetProperty(p => p.StockQuantity, p => p.StockQuantity + context.OrderItems
                                .Where(oi => oi.ProductId == p.Id && oi.Order.Status == OrderStatus.Pending && oi.Order.OrderDate.CompareTo(expirationTime) <= 0)
                                .Sum(oi => oi.Quantity)), stoppingToken);
                    
                    await context.Orders.Where(o => o.Status == OrderStatus.Pending && o.OrderDate.CompareTo(expirationTime) <= 0).ExecuteDeleteAsync(stoppingToken);
                    await transaction.CommitAsync(stoppingToken);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(stoppingToken);
                    _logger.LogInformation("Clean unpaid orders task encounter an error, rollback... {}", ex);
                    throw;
                }
            });
            

            _logger.LogInformation("Clean unpaid orders task completed processing.");

            await Task.Delay(TimeSpan.FromMinutes(5), stoppingToken);
        }
        
    }
}

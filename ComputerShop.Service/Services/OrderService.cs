using System;
using ComputerShop.Repository.CustomExceptions;
using ComputerShop.Repository.Enums;
using ComputerShop.Repository.Models;
using ComputerShop.Repository.Repositories;

namespace ComputerShop.Service.Services;

public class OrderService
{
    private readonly UnitOfWork _unitOfWork;

    public OrderService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<Order> HandleCreateOrder(Guid userId, OrderType type, string? shippingAddress, string? trackingPhone, string? notes, Dictionary<Guid, int> itemParam)
    {
        var customer = await _unitOfWork.UserRepository.FindUserByIdAsync(userId);
        if (customer == null)
        {
            throw new UnauthorizedException("User is not authenticated.");
        }

        List<OrderItem> orderItems = [];
        var orderId = Guid.NewGuid();
        decimal subTotal = 0;
        decimal shippingCost = 0;
        decimal tax = 0;

        foreach (KeyValuePair<Guid, int> entry in itemParam)
        {
            var product = await _unitOfWork.ProductRepository.FindProductByIdAsync(entry.Key);
            if (product == null)
            {
                throw new NotFoundException($"Product {entry.Key} not found.");
            }
            if (entry.Value <= 0)
            {
                throw new BadRequestException($"Requested item quantity for product {product.Id} cannot be lower than 1.");
            }
            if (product.StockQuantity < entry.Value)
            {
                throw new BadRequestException($"The remaining stock for the product {product.Id} is not enough.");
            }
            
            var itemSubTotal = product.Price * entry.Value;
            var orderItem = new OrderItem
            {
                OrderId = orderId,
                ProductId = product.Id,
                Quantity = entry.Value,
                UnitPrice = product.Price,
                SubTotal = itemSubTotal,
            };
            orderItems.Add(orderItem);
            subTotal += itemSubTotal;
        }
        var order = new Order
        {
            Id = orderId,
            UserId = userId,
            SubTotal = subTotal,
            ShippingCost = shippingCost,
            Tax = tax,
            TotalAmount = subTotal + shippingCost + tax,
        };
        return null;
    }
}

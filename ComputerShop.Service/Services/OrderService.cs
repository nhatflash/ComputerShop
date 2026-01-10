using System;
using ComputerShop.Repository.CustomExceptions;
using ComputerShop.Repository.Enums;
using ComputerShop.Repository.Models;
using ComputerShop.Repository.Repositories;
using ComputerShop.Service.Businesses;

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

        var customer = await _unitOfWork.UserRepository.FindUserByIdAsync(userId) ?? throw new UnauthorizedException("User is not authenticated.");

        ValidateOrderTypeAndShippingAddress(type, shippingAddress);
        var validPhoneNumber = GetTrackingPhoneNumberFromRequest(trackingPhone, customer);

        List<OrderItem> orderItems = [];
        var orderId = Guid.NewGuid();

        decimal subTotal = 0;
        decimal shippingCost = BusinessConstant.ShippingCost;
        decimal tax = BusinessConstant.Tax;

        foreach (KeyValuePair<Guid, int> entry in itemParam)
        {
            var productId = entry.Key;
            var quantity = entry.Value;

            var product = await _unitOfWork.ProductRepository.FindProductByIdWithTrackingAsync(productId) ?? throw new NotFoundException($"Product {productId} not found.");

            ValidateAddOrderItem(product, quantity);
            
            var itemSubTotal = product.Price * quantity;
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
            product.StockQuantity -= quantity;
            if (product.StockQuantity <= 0)
            {
                product.Status = ProductStatus.Unavailable;
            }
        }

        var totalAmount = subTotal + shippingCost + tax;
        var order = new Order
        {
            Id = orderId,
            UserId = userId,
            SubTotal = subTotal,
            ShippingCost = shippingCost,
            Tax = tax,
            TotalAmount = totalAmount,
            Type = type,
            ShippingAddress = shippingAddress,
            TrackingPhone = validPhoneNumber,
            Status = OrderStatus.Pending,
            OrderItems = orderItems,
            Notes = notes,
        };

        await _unitOfWork.OrderRepository.CreateOrderAsync(order);
        return order;
    }


    private static void ValidateAddOrderItem(Product product, int quantity)
    {
        if (quantity <= 0)
        {
            throw new BadRequestException($"Requested item quantity for product {product.Id} cannot be lower than 0");
        }
        if (product.StockQuantity < quantity)
        {
            throw new BadRequestException($"The remaining stock for the product {product.Id} is not enough with the requested item quantity.");
        }
    }

    
    private static void ValidateOrderTypeAndShippingAddress(OrderType type, string? shippingAddress)
    {
        if (type == OrderType.Delivery)
        {
            if (string.IsNullOrWhiteSpace(shippingAddress))
            {
                throw new BadRequestException("Shipping address must be specified when placing order with delivery type.");
            }
        }
        if (type == OrderType.PickUp)
        {
            if (!string.IsNullOrWhiteSpace(shippingAddress))
            {
                throw new BadRequestException("Shipping address only available when the order has type delivery.");
            }
        }
    }

    private static string GetTrackingPhoneNumberFromRequest(string? requestedTrackingPhone, User customer)
    {
        if (string.IsNullOrWhiteSpace(requestedTrackingPhone))
        {
            if (customer.PhoneNumber == null)
            {
                throw new BadRequestException("Tracking phone number can not be left empty if user does not have phone number information.");
            }
            return customer.PhoneNumber;
        }
        return requestedTrackingPhone;
    }
    
}

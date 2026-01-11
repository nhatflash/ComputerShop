using ComputerShop.Repository.Enums;

namespace ComputerShop.Repository.Models;

public class Order
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public DateTimeOffset OrderDate { get; set; }

    public decimal SubTotal { get; set; }

    public decimal ShippingCost { get; set; }

    public decimal Tax { get; set; }

    public decimal TotalAmount { get; set; }

    public OrderType Type { get; set; }

    public string? ShippingAddress { get; set; }

    public string TrackingPhone { get; set; } = string.Empty;

    public OrderStatus Status { get; set; }

    public string? Notes { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; } = [];
}  

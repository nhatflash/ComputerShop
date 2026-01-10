using System;

namespace ComputerShop.Repository.Models;

public class OrderItem
{
    public long Id { get; set; }

    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotal { get; set; }

    public Order Order { get; set; } = default!;

    public Product Product { get; set; } = default!;
}

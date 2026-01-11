using System;

namespace ComputerShop.Repository.Models;

public class CartItem
{
    public Guid Id { get; set; }

    public Guid CartId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTimeOffset AttachedAt { get; set; } = DateTimeOffset.Now;

    public Cart Cart { get; set; } = default!;

    public Product Product { get; set; } = default!;
}

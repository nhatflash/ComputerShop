using System;

namespace ComputerShop.Repository.Models;

public class Review
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = string.Empty;

    public string? Attachment { get; set; }

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.Now;

    public User User { get; set; } = default!;

    public Product Product { get; set; } = default!;
}

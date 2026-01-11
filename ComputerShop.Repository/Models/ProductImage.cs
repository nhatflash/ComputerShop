namespace ComputerShop.Repository.Models;

public class ProductImage
{
    public long Id { get; set; }

    public Guid ProductId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; }

    public Product Product { get; set; } = default!;
}

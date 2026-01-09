using System;
using ComputerShop.Repository.Enums;

namespace ComputerShop.Repository.Models;

public class Product
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }

    public Guid ManufacturerId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Dictionary<string, string> Specifications { get; set; } = new();

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public ProductStatus Status { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public DateTimeOffset UpdatedAt { get; set; }


    public Category Category { get; set; } = default!;

    public Manufacturer Manufacturer { get; set; } = default!;

    public ICollection<ProductImage> ProductImages { get; set; } = [];
}



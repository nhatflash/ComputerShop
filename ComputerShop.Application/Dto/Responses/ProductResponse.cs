using System;
using ComputerShop.Repository.Enums;

namespace ComputerShop.Application.Dto.Responses;

public class ProductResponse
{
    public Guid Id { get; set; }

    public Guid CategoryId { get; set; }
    
    public Guid ManufacturerId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public Dictionary<string, string> Specifications { get; set; } = [];

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public ProductStatus Status { get; set; }

    public List<ProductImageResponse> ProductImages { get; set; } = [];
}

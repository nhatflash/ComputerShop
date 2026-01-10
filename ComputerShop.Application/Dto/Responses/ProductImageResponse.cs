using System;

namespace ComputerShop.Application.Dto.Responses;

public class ProductImageResponse
{
    public long Id { get; set; }

    public Guid ProductId { get; set; }

    public string ImageUrl { get; set; } = string.Empty;
}

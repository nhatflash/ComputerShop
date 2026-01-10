using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Application.Dto.Requests;

public class UpdateProductRequest
{
    [StringLength(256, ErrorMessage = "Product name must not exceed 256 characters.)")]
    public string? Name { get; set; }

    public string? Description { get; set; }

    public Dictionary<string, string>? Specifications { get; set; }

    public decimal? Price { get; set; }

    public int? WarrantyMonth { get; set; }
}

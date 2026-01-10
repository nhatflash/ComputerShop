using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Application.Dto.Requests;

public class AddProductRequest
{
    [Required(ErrorMessage = "Category Id is required.")]
    public Guid CategoryId { get; set; }

    [Required(ErrorMessage = "Manufacturer Id is required.")]
    public Guid ManufacturerId { get; set; }

    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(256, ErrorMessage = "Product name must not exceed 256 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = string.Empty;

    [Required(ErrorMessage = "Specification is required.")]
    public Dictionary<string, string> Specifications { get; set; } = new();

    [Required(ErrorMessage = "Price is required.")]
    public decimal Price { get; set; }

}

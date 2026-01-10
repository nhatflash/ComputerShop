using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Application.Dto.Requests;

public class AddProductImageRequest
{
    [Required(ErrorMessage = "Product Id is required.")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Product image url is required.")]
    public string ImageUrl { get; set; } = string.Empty;
}

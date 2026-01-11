using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Application.Dto.Requests;

public class AddMultipleProductQuantityRequest
{
    [Required(ErrorMessage = "Product info is required")]
    public List<AddProductQuantityRequest> Products { get; set; } = [];
}

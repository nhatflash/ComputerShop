using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Application.Dto.Requests;

public class AddProductQuantityRequest
{
    [Required(ErrorMessage = "Product Id is required.")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Product quantity is required.")]
    public int Quantity { get; set; }
}

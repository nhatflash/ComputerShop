using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Application.Dto.Requests;

public class AddOrderItemRequest
{
    [Required(ErrorMessage = "Product Id is required.")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Item quantity is required.")]
    public int Quantity { get; set; }
}

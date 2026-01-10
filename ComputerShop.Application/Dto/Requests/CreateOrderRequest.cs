using System;
using System.ComponentModel.DataAnnotations;
using ComputerShop.Repository.Enums;

namespace ComputerShop.Application.Dto.Requests;

public class CreateOrderRequest
{
    [Required(ErrorMessage = "Order items is required.")]
    public List<AddOrderItemRequest> OrderItems { get; set; } = [];

    [Required(ErrorMessage = "Order type is required.")]
    public OrderType Type { get; set; }

    public string? ShippingAddress { get; set; }

    public string? TrackingPhone { get; set; }

    public string? Notes { get; set; }
}

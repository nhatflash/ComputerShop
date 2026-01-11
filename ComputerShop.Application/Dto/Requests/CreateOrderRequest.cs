using System;
using System.ComponentModel.DataAnnotations;
using ComputerShop.Application.Common;
using ComputerShop.Repository.Enums;

namespace ComputerShop.Application.Dto.Requests;

public class CreateOrderRequest
{
    [Required(ErrorMessage = "Order items is required.")]
    public List<AddOrderItemRequest> OrderItems { get; set; } = [];

    [Required(ErrorMessage = "Order type is required.")]
    public OrderType Type { get; set; }

    [StringLength(256, ErrorMessage = "Shipping address must not exceed 256 characters.")]
    public string? ShippingAddress { get; set; }

    [StringLength(20, ErrorMessage = "Tracking phone must not exceed 20 characters.")]
    [RegularExpression(
        pattern: CustomPattern.PhonePattern,
        ErrorMessage = "Invalid phone number format.",
        MatchTimeoutInMilliseconds = 250
    )]
    public string? TrackingPhone { get; set; }

    [StringLength(256, ErrorMessage = "Order notes must not exceed 256 characters.")]
    public string? Notes { get; set; }
}

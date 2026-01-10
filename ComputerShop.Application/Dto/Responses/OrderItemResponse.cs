using System;

namespace ComputerShop.Application.Dto.Responses;

public class OrderItemResponse
{
    public long Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal SubTotal { get; set; }
}

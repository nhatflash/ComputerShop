using ComputerShop.Repository.Enums;

namespace ComputerShop.Repository.Models;

public class Payment
{
    public Guid Id { get; set; }

    public Guid OrderId { get; set; }

    public decimal Amount { get; set; }

    public PaymentMethod Method { get; set; }

    public PaymentStatus Status { get; set; }

    public DateTimeOffset PaymentDate { get; set; }

    public Order Order { get; set; } = default!;
}

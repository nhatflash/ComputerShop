using ComputerShop.Repository.CustomExceptions;
using ComputerShop.Repository.Repositories;
using Microsoft.Extensions.Configuration;
using PayOS;
using PayOS.Models.V2.PaymentRequests;

namespace ComputerShop.Service.Services;

public class PaymentService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly IConfiguration _configuration;

    public PaymentService(UnitOfWork unitOfWork, IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }


    public async Task<string> HandleCreatePayOsCheckoutUrl(Guid orderId, Guid userId)
    {
        var order = await _unitOfWork.OrderRepository.FindOrderByIdAsync(orderId) ?? throw new NotFoundException("No order found.");

        if (order.UserId != userId)
        {
            throw new UnauthorizedException("The requested order does not belong to you.");
        }

        var payOS = new PayOSClient(
            _configuration["PayOS:ClientID"]!,
            _configuration["PayOS:APIKey"]!,
            _configuration["PayOS:ChecksumKey"]!
        );

        var orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
        decimal roundedTotalAmount = Math.Round(order.TotalAmount);
        long paymentAmount = Convert.ToInt64(roundedTotalAmount);
        var paymentRequest = new CreatePaymentLinkRequest
        {
            OrderCode = orderCode,
            Amount = paymentAmount,
            Description = $"Thanh toán đơn hàng {orderId}",
            ReturnUrl = _configuration["PayOS:ReturnUrl"]!,
            CancelUrl = _configuration["PayOS:CancelUrl"]!
        };

        var paymentLink = await payOS.PaymentRequests.CreateAsync(paymentRequest);
        return paymentLink.CheckoutUrl;
    }
}

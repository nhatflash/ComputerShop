using ComputerShop.Repository.CustomExceptions;
using ComputerShop.Repository.Enums;
using ComputerShop.Repository.Models;
using ComputerShop.Repository.Repositories;
using ComputerShop.Service.Constant;
using ComputerShop.Service.Utils;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using PayOS;
using PayOS.Models.V2.PaymentRequests;
using PayOS.Models.Webhooks;

namespace ComputerShop.Service.Services;

public class PaymentService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly PayOSClient _payOS;
    private readonly IConfiguration _configuration;
    private readonly RedisUtils _redisUtils;

    public PaymentService(UnitOfWork unitOfWork, PayOSClient payOS, IConfiguration configuration, RedisUtils redisUtils)
    {
        _unitOfWork = unitOfWork;
        _payOS = payOS;
        _configuration = configuration;
        _redisUtils = redisUtils;
    }


    public async Task<string> HandleCreatePayOsCheckoutUrl(Guid orderId, Guid userId)
    {
        var order = await _unitOfWork.OrderRepository.FindOrderByIdAsync(orderId) ?? throw new NotFoundException("No order found.");

        if (order.UserId != userId)
        {
            throw new UnauthorizedException("The requested order does not belong to you.");
        }

        long orderCode = long.Parse(DateTimeOffset.Now.ToString("ffffff"));
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

        var paymentLink = await _payOS.PaymentRequests.CreateAsync(paymentRequest);
        var orderKey = RedisKey.GetPayOSOrderKey(orderCode);
        await _redisUtils.StoreGuidData(orderKey, orderId, TimeSpan.FromMinutes(15));
        return paymentLink.CheckoutUrl;
    }


    public async Task HandlePayOSWebhookData(Webhook webhook)
    {
        var verifiedData = await _payOS.Webhooks.VerifyAsync(webhook);

        if (verifiedData.Code == "00")
        {
            var orderKey = RedisKey.GetPayOSOrderKey(verifiedData.OrderCode);
            var orderId = await _redisUtils.GetGuidDataFromKey(orderKey);
            var order = await _unitOfWork.OrderRepository.FindOrderByIdWithTrackingAsync(orderId) ?? throw new NotFoundException($"No order found {orderId}");

            order.Status = OrderStatus.Awaiting;
            decimal amount = Convert.ToDecimal(verifiedData.Amount);
            await HandleCreatePayment(orderId, amount, PaymentMethod.PayOS);
        }
    }


    private async Task HandleCreatePayment(Guid orderId, decimal amount, PaymentMethod method)
    {
        var payment = new Payment
        {
            Id = Guid.NewGuid(),
            OrderId = orderId,
            Amount = amount,
            Method = method,
        };
        await _unitOfWork.PaymentRepository.CreatePaymentAsync(payment);
    }
}

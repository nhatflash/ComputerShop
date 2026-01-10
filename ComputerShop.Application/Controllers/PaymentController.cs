using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using PayOS;
using PayOS.Models.V2.PaymentRequests;

namespace ComputerShop.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public PaymentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        
        [HttpPost("payos")]
        public async Task<IActionResult> CreatePayOSPaymentLink()
        {
            var payOS = new PayOSClient(
                _configuration["PayOS:ClientID"]!,
                _configuration["PayOS:APIKey"]!,
                _configuration["PayOS:ChecksumKey"]!
            );
            int orderCode = int.Parse(DateTimeOffset.Now.ToString("ffffff"));
            Guid productId = Guid.NewGuid();

            var paymentRequest = new CreatePaymentLinkRequest
            {
                OrderCode = orderCode,
                Amount = 2000,
                Description = "Thanh toán đơn hàng",
                CancelUrl = "https://localhost:7180/payos-cancel",
                ReturnUrl = "https://localhost:7180/payos-success"
            };
            var paymentLink = await payOS.PaymentRequests.CreateAsync(paymentRequest);
            return Ok(paymentLink.CheckoutUrl);
        }
    }
}

using ComputerShop.Application.Common;
using ComputerShop.Service.Context;
using ComputerShop.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PayOS.Models.Webhooks;

namespace ComputerShop.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {

        private readonly ServiceProviders _serviceProvider;
        private readonly UserContext _userContext;

        public PaymentController(ServiceProviders serviceProvider, UserContext userContext)
        {
            _serviceProvider = serviceProvider;
            _userContext = userContext;
        }

        
        [HttpPost("payos/{orderId}")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreatePayOSCheckOutUrl([FromRoute] Guid orderId)
        {
            var currentUserId = _userContext.GetCurrentAuthenticatedUserId();
            var checkoutUrl = await _serviceProvider.PaymentService.HandleCreatePayOsCheckoutUrl(orderId, currentUserId);
            var response = new ApiResponse<string>
            { 
                StatusCode = StatusCodes.Status200OK, 
                Value = checkoutUrl,
            };
            return Ok(response);
        }


        [HttpGet("/payos-success")]
        public async Task<IActionResult> PayOSSuccess()
        {
            var response = new ApiResponse<string>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = "Thanh toán thành công"
            };
            return Ok(response);
        }


        [HttpGet("/payos-cancel")]
        public async Task<IActionResult> PayOSCancel()
        {
            var response = new ApiResponse<string>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = "Thanh toán thất bại"
            };
            return Ok(response);
        }


        [HttpPost("/payos-webhook")]
        public async Task<IActionResult> ReceivePayOSWebhook([FromBody] Webhook webhook)
        {
            await _serviceProvider.PaymentService.HandlePayOSWebhookData(webhook);
            var response = new ApiResponse<string>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = "Nhận phản hồi thành công"
            };
            return Ok(response);
        }
    }
}

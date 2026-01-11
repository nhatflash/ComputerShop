using ComputerShop.Application.Common;
using ComputerShop.Application.Dto.Requests;
using ComputerShop.Application.Dto.Responses;
using ComputerShop.Repository.Models;
using ComputerShop.Service.Context;
using ComputerShop.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShop.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        
        private readonly ServiceProviders _serviceProvider;
        private readonly UserContext _userContext;

        public OrderController(ServiceProviders serviceProvider, UserContext userContext)
        {
            _serviceProvider = serviceProvider;
            _userContext = userContext;
        }

        [HttpPost]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> CreateOrder([FromBody] CreateOrderRequest request)
        {
            if (!ModelState.IsValid) {
                return BadRequest("Create order request is not valid.");
            }
            Dictionary<Guid, int> itemParam = [];
            foreach (var item in request.OrderItems)
            {
                if (!itemParam.ContainsKey(item.ProductId))
                {
                    itemParam.Add(item.ProductId, item.Quantity);
                }
                else
                {
                    var additionalQuantity = item.Quantity;
                    itemParam[item.ProductId] += additionalQuantity;
                }
            }
            var currentUserId = _userContext.GetCurrentAuthenticatedUserId();
            var order = await _serviceProvider.OrderService.HandleCreateOrder(currentUserId, 
                                                                              request.Type, 
                                                                              request.ShippingAddress, 
                                                                              request.TrackingPhone, 
                                                                              request.Notes, 
                                                                              itemParam);
            var response = new ApiResponse<OrderResponse>
            {
                StatusCode = StatusCodes.Status201Created,
                Value = ResponseMapper.MapToOrderResponse(order)
            };
            return Ok(response);
        }

    }
}

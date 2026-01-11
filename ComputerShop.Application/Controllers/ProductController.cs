using ComputerShop.Application.Common;
using ComputerShop.Application.Dto.Requests;
using ComputerShop.Application.Dto.Responses;
using ComputerShop.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShop.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ServiceProviders _serviceProvider;

        public ProductController(ServiceProviders serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Add product request is not valid.");
            }
            var product = await _serviceProvider.ProductService.HandleAddProduct(request.CategoryId, 
                                                                                 request.ManufacturerId, 
                                                                                 request.Name, 
                                                                                 request.Description, 
                                                                                 request.Specifications, 
                                                                                 request.Price, 
                                                                                 request.WarrantyMonth, 
                                                                                 request.ImageUrls);
            var response = new ApiResponse<ProductResponse>
            {
                StatusCode = StatusCodes.Status201Created,
                Value = ResponseMapper.MapToProductResponse(product),
            };
            return Created("created", response);
        }


        [HttpGet("{productId}")]
        public async Task<IActionResult> FindProduct([FromRoute] Guid productId)
        {
            var product = await _serviceProvider.ProductService.FindProductById(productId);
            var response = new ApiResponse<ProductResponse>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = ResponseMapper.MapToProductResponse(product),
            };
            return Ok(response);
        }

        [HttpPatch("{productId}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateProduct([FromRoute] Guid productId, [FromBody] UpdateProductRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Update product request is not valid.");
            }
            var product = await _serviceProvider.ProductService.HandleUpdateProduct(productId, 
                                                                                    request.Name, 
                                                                                    request.Description, 
                                                                                    request.Specifications, request.Price, 
                                                                                    request.WarrantyMonth);
            var response = ResponseMapper.MapToProductResponse(product);
            return Ok(new ApiResponse<ProductResponse>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = response,
            });
        }
    }
}

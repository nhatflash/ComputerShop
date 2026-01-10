using ComputerShop.Application.Common;
using ComputerShop.Application.Dto.Requests;
using ComputerShop.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            var product = await _serviceProvider.ProductService.HandleAddProduct(request.CategoryId, request.ManufacturerId, request.Name, request.Description, request.Specifications, request.Price);
            var response = ResponseMapper.MapToProductResponse(product);
            return Created("created", response);
        }


        [HttpGet("{productId}")]
        public async Task<IActionResult> FindProduct([FromRoute] Guid productId)
        {
            var product = await _serviceProvider.ProductService.FindProductById(productId);
            var response = ResponseMapper.MapToProductResponse(product);
            return Ok(response);
        }
    }
}

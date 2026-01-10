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
    public class CategoryController : ControllerBase
    {
        private readonly ServiceProviders _serviceProvider;

        public CategoryController(ServiceProviders serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Add category request is not valid.");
            }
            var category = await _serviceProvider.CategoryService.HandleAddCategory(request.Name, request.Description, request.ImageUrl);
            var response = ResponseMapper.MapToCategoryResponse(category);
            return Created("created", response);
        }
    }
}

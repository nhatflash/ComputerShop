using ComputerShop.Application.Common;
using ComputerShop.Application.Dto.Requests;
using ComputerShop.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ComputerShop.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManufacturerController : ControllerBase
    {
        
        private readonly ServiceProviders _serviceProvider;

        public ManufacturerController(ServiceProviders serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddManufacturer([FromBody] AddManufacturerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid add manufacturer request.");
            }
            var manufacturer = await _serviceProvider.ManufacturerService.HandleAddManufacturer(request.Name);
            var response = ResponseMapper.MapToManufacturerResponse(manufacturer);
            return Created("created", response);
        }
    }
}

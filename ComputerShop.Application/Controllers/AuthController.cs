using ComputerShop.Application.Dto.Requests;
using ComputerShop.Application.Dto.Responses;
using ComputerShop.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace ComputerShop.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ServiceProviders _serviceProvider;

        public AuthController(ServiceProviders serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        
        [HttpPost("signin")]
        public async Task<IActionResult> SignIn([FromBody] SignInRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Sign in request is not valid.");
            }
            var result = await _serviceProvider.AuthService.HandleSignIn(request.Login, request.Password);
            return Ok(new ApiResponse<SignInResponse>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = new SignInResponse
                {
                    AccessToken = result["accessToken"]
                }
            });
        }
    }
}

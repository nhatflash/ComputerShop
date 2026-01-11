using ComputerShop.Application.Common;
using ComputerShop.Application.Dto.Requests;
using ComputerShop.Application.Dto.Responses;
using ComputerShop.Service.Services;
using Microsoft.AspNetCore.Mvc;

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
            var response = new ApiResponse<SignInResponse>
            {
                StatusCode = StatusCodes.Status200OK,
                Value = ResponseMapper.MapToSignInResponse(result)
            };
            return Ok(response);
        }


        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Sign up request is not valid.");
            }

            var user = await _serviceProvider.AuthService.HandleSignUp(request.Email, 
                                                                      request.Password, 
                                                                      request.ConfirmedPassword, 
                                                                      request.FirstName, 
                                                                      request.LastName);
            var response = new ApiResponse<UserResponse>
            {
                StatusCode = StatusCodes.Status201Created,
                Value = ResponseMapper.MapToUserResponse(user)
            };
            return Created("/signup", response);
        }


        
    }
}

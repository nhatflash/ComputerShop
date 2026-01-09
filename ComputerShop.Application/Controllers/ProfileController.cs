using ComputerShop.Application.Common;
using ComputerShop.Application.Dto.Requests;
using ComputerShop.Repository.Enums;
using ComputerShop.Service.Context;
using ComputerShop.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerShop.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        
        private readonly ServiceProviders _serviceProvider;
        private readonly UserContext _userContext;

        public ProfileController(ServiceProviders serviceProvider, UserContext userContext)
        {
            _serviceProvider = serviceProvider;
            _userContext = userContext;
        }


        [Authorize(Roles = "Customer")]
        [HttpPatch("complete")]
        public async Task<IActionResult> CompleteProfileInfo([FromBody] CompleteProfileInfoRequest request)
        {
            var currentUserId = _userContext.GetCurrentAuthenticatedUserId();
            var user = await _serviceProvider.UserService.HandleCompleteProfileInfo(currentUserId, request.PhoneNumber, request.Gender, request.Address, request.Ward, request.District, request.City, request.Province, request.PostalCode, request.ProfileImage);
            var response = ResponseMapper.MapToUserResponse(user);
            return Ok(response);
        }


        [HttpPatch]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileRequest request)
        {
            var currentUserId = _userContext.GetCurrentAuthenticatedUserId();
            var user = await _serviceProvider.UserService.HandleUpdateProfile(currentUserId, request.FirstName, request.LastName, request.PhoneNumber, request.Address, request.Ward, request.District, request.City, request.Province, request.PostalCode, request.ProfileImage);
            var response = ResponseMapper.MapToUserResponse(user);
            return Ok(response);
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> RetrieveProfile() {
            var currentUserId = _userContext.GetCurrentAuthenticatedUserId();
            var user = await _serviceProvider.UserService.FindUserById(currentUserId);
            var response = ResponseMapper.MapToUserResponse(user);
            return Ok(response);
        }


        

    }
}

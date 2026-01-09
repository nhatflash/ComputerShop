using System;
using ComputerShop.Application.Dto.Responses;
using ComputerShop.Repository.Models;

namespace ComputerShop.Application.Common;

public class ResponseMapper
{


    public static SignInResponse MapToSignInResponse(Dictionary<string, string> param)
    {
        return new SignInResponse
        {
            AccessToken = param["accessToken"],
            RefreshToken = param["refreshToken"]
        };
    }


    public static UserResponse MapToUserResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Gender = user.Gender,
            Address = user.Address,
            Ward = user.Ward,
            District = user.District,
            City = user.City,
            Province = user.Province,
            PostalCode = user.PostalCode,
            ProfileImage = user.ProfileImage,
            VerifiedAt = user.VerifiedAt,
            CreatedAt = user.CreatedAt,
        };
    }
}

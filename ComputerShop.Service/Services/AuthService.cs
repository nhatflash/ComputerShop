using System;
using ComputerShop.Repository.CustomExceptions;
using ComputerShop.Repository.Models;
using ComputerShop.Repository.Repositories;
using ComputerShop.Service.Utils;

namespace ComputerShop.Service.Services;


public class AuthService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly JwtUtils _jwtUtils;

    public AuthService(UnitOfWork unitOfWork, JwtUtils jwtUtils)
    {
        _unitOfWork = unitOfWork;
        _jwtUtils = jwtUtils;
    }

    public async Task<Dictionary<string, string>> HandleSignIn(string login, string password)
    {
        User? user;
        if (login.Contains('@'))
        {
            user = await _unitOfWork.UserRepository.FindUserByEmailAsync(login);
        }
        else
        {
            user = await _unitOfWork.UserRepository.FindUserByPhoneAsync(login);
        }
        
        if (user == null)
        {
            throw new UnauthorizedException("Invalid credentials.");
        }

        if (user.PasswordHash != password)
        {
            throw new UnauthorizedException("Invalid credentials.");
        }

        var tokenMap = new Dictionary<string, string>
        {
            {"accessToken", _jwtUtils.GenerateAccessToken(user)}
        };

        return tokenMap;
    }

}

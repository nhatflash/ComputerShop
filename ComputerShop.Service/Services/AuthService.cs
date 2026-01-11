using ComputerShop.Repository.CustomExceptions;
using ComputerShop.Repository.Enums;
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

        if (!PasswordEncoder.VerifyPassword(password, user.PasswordHash))
        {
            throw new UnauthorizedException("Invalid credentials.");
        }

        var tokenMap = new Dictionary<string, string>
        {
            {"accessToken", _jwtUtils.GenerateAccessToken(user)},
            {"refreshToken", _jwtUtils.GenerateRefreshToken(user)}
        };

        return tokenMap;
    }


    public async Task<User> HandleSignUp(string email, string password, string confirmedPassword, string firstName, string lastName)
    {
        if (await _unitOfWork.UserRepository.UserExistByEmail(email))
        {
            throw new BadRequestException("This email is already in use.");
        }

        if (password != confirmedPassword)
        {
            throw new BadRequestException("Confirmed password does not match.");
        }

        User user = new User
        {
            Id = Guid.NewGuid(),
            Email = email,
            PasswordHash = PasswordEncoder.HashPassword(password),
            Role = UserRole.Customer,
            FirstName = firstName,
            LastName = lastName,
            Status = UserStatus.Active,
        };

        await _unitOfWork.UserRepository.AddUserAsync(user);
        return user;
    }

}

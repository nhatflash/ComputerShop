using System;
using ComputerShop.Repository.CustomExceptions;
using ComputerShop.Repository.Enums;
using ComputerShop.Repository.Models;
using ComputerShop.Repository.Repositories;

namespace ComputerShop.Service.Services;

public class UserService
{

    private readonly UnitOfWork _unitOfWork;

    public UserService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<User> HandleCompleteProfileInfo(Guid userId, string phone, Gender? gender, string address, string ward, string district, string city, string province, string postalCode, string? profileImage)
    {
        if (await _unitOfWork.UserRepository.UserExistByPhone(phone))
        {
            throw new BadRequestException("This phone number is already in use.");
        }

        var user = await _unitOfWork.UserRepository.FindUserByIdWithTrackingAsync(userId);
        if (user == null)
        {
            throw new UnauthorizedException("User is not authenticated.");
        }

        if (user.VerifiedAt.HasValue)
        {
            throw new BadRequestException("User has already completed profile information.");
        }

        user.PhoneNumber = phone;
        user.Gender = gender;
        user.Address = address;
        user.Ward = ward;
        user.District = district;
        user.City = city;
        user.Province = province;
        user.PostalCode = postalCode;
        user.ProfileImage = profileImage;
        user.VerifiedAt = DateTimeOffset.Now;

        await _unitOfWork.SaveChangesAsync();

        return user;
    }


    public async Task<User> HandleUpdateProfile(Guid userId, string? firstName, string? lastName, string? phone, string? address, string? ward, string? district, string? city, string? province, string? postalCode, string? profileImage)
    {
        var user = await _unitOfWork.UserRepository.FindUserByIdWithTrackingAsync(userId);
        if (user == null)
        {
            throw new UnauthorizedException("User is not authenticated.");
        }

        if (!user.VerifiedAt.HasValue)
        {
            throw new BadRequestException("You have to verify your account before update the profile.");
        }

        return await UpdateProfileWithNewInformation(user, firstName, lastName, phone, address, ward, district, city, province, postalCode, profileImage);
    }



    public async Task<User> FindUserById(Guid id)
    {
        var user = await _unitOfWork.UserRepository.FindUserByIdAsync(id);
        if (user == null)
        {
            throw new NotFoundException("User not found.");
        }
        return user;
    }


    private async Task<User> UpdateProfileWithNewInformation(User user, string? firstName, string? lastName, string? phone, string? address, string? ward, string? district, string? city, string? province, string? postalCode, string? profileImage)
    {
        if (!string.IsNullOrWhiteSpace(firstName))
        {
            user.FirstName = firstName;
        }
        if (!string.IsNullOrWhiteSpace(lastName))
        {
            user.LastName = lastName;
        }
        if (!string.IsNullOrWhiteSpace(phone))
        {
            if (user.PhoneNumber != phone && await _unitOfWork.UserRepository.UserExistByPhone(phone))
            {
                throw new BadRequestException("This phone number is already in use.");
            }
            user.PhoneNumber = phone;
        }
        if (!string.IsNullOrWhiteSpace(address))
        {
            user.Address = address;
        }
        if (!string.IsNullOrWhiteSpace(ward))
        {
            user.Ward = ward;
        }
        if (!string.IsNullOrWhiteSpace(district))
        {
            user.District = district;
        }
        if (!string.IsNullOrWhiteSpace(city))
        {
            user.City = city;
        }
        if (!string.IsNullOrWhiteSpace(province))
        {
            user.Province = province;
        }
        if (!string.IsNullOrWhiteSpace(postalCode))
        {
            user.PostalCode = postalCode;
        }
        user.ProfileImage = profileImage;

        await _unitOfWork.SaveChangesAsync();
        return user;
    }
}

using System;
using Microsoft.AspNetCore.Http;

namespace ComputerShop.Repository.CustomExceptions;

public class BadRequestException : BaseException
{

    public BadRequestException(string message) : base(message, StatusCodes.Status400BadRequest){}
}

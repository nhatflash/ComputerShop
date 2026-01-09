using System;
using Microsoft.AspNetCore.Http;

namespace ComputerShop.Repository.CustomExceptions;

public class UnauthorizedException : BaseException
{
    public UnauthorizedException(string message) : base(message, StatusCodes.Status401Unauthorized){}
}

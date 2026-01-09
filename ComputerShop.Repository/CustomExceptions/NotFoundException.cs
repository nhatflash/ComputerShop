using System;
using Microsoft.AspNetCore.Http;

namespace ComputerShop.Repository.CustomExceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(string message) : base(message, StatusCodes.Status404NotFound)
    {
    }
}

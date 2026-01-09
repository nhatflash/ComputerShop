using System;

namespace ComputerShop.Repository.CustomExceptions;

public class BaseException : Exception
{
    public int StatusCode {get;}

    public BaseException(string message, int statusCode) : base(message)
    {
        StatusCode = statusCode;
    }
}

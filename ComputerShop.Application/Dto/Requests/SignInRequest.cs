using System;

namespace ComputerShop.Application.Dto.Requests;

public class SignInRequest
{
    public string Login { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}

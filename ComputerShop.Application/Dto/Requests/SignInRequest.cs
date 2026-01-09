using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Application.Dto.Requests;

public class SignInRequest
{
    [Required(ErrorMessage = "Email/Phone number is required.")]
    public string Login { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    public string Password { get; set; } = string.Empty;
}

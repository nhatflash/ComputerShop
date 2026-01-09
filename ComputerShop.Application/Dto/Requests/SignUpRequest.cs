using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Application.Dto.Requests;

public class SignUpRequest
{
    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Must provide a valid email address.")]
    [StringLength(256, ErrorMessage = "Email address must not exceed 256 characters.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(256, ErrorMessage = "Password must not exceed 256 characters.")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Confirmed password is required.")]
    public string ConfirmedPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "First name is required.")]
    [StringLength(256, ErrorMessage = "First name must not exceed 256 characters")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Last name is required.")]
    [StringLength(256, ErrorMessage = "Last name must not exceed 256 characters.")]
    public string LastName { get; set; } = string.Empty;
}



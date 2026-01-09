using System;
using System.ComponentModel.DataAnnotations;
using ComputerShop.Application.Common;

namespace ComputerShop.Application.Dto.Requests;

public class UpdateProfileRequest
{
    
    [StringLength(256, ErrorMessage = "First name must not exceed 256 characters.")]
    public string? FirstName { get; set; }


    [StringLength(256, ErrorMessage = "Last name must not exceed 256 characters.")]
    public string? LastName { get; set; }

    [StringLength(20, ErrorMessage = "Phone number must not exceed 20 characters.")]
    [RegularExpression(
        pattern: CustomPattern.PhonePattern,
        ErrorMessage = "Invalid phone number.",
        MatchTimeoutInMilliseconds = 250
    )]
    public string? PhoneNumber { get; set; }

    [StringLength(256, ErrorMessage = "Address must not exceed 256 characters.")]
    public string? Address { get; set; }

    [StringLength(100, ErrorMessage = "Ward must not exceed 100 characters.")]
    public string? Ward { get; set; }

    [StringLength(100, ErrorMessage = "District must not exceed 100 characters.")]
    public string? District { get; set; }

    [StringLength(100, ErrorMessage = "City must not exceed 100 characters.")]
    public string? City { get; set; }

    [StringLength(100, ErrorMessage = "Province must not exceed 100 characters.")]
    public string? Province { get; set; }

    [StringLength(20, ErrorMessage = "Postal code must not exceed 20 characters.")]
    [RegularExpression(
        pattern: CustomPattern.PostalCodePattern,
        ErrorMessage = "Invalid postal code.",
        MatchTimeoutInMilliseconds = 250
    )]
    public string? PostalCode { get; set; }

    public string? ProfileImage { get; set; }
}

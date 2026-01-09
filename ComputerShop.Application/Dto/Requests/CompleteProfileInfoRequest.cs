using System;
using System.ComponentModel.DataAnnotations;
using ComputerShop.Application.Common;
using ComputerShop.Repository.Enums;

namespace ComputerShop.Application.Dto.Requests;

public class CompleteProfileInfoRequest
{
    [Required(ErrorMessage = "Phone number is required.")]
    [StringLength(20, ErrorMessage = "Phone number must not exceed 20 characters.")]
    [RegularExpression(
        pattern: CustomPattern.PhonePattern,
        ErrorMessage = "Invalid phone number format.", 
        MatchTimeoutInMilliseconds = 250)]
    public string PhoneNumber { get; set; } = string.Empty;

    public Gender? Gender { get; set; }

    [Required(ErrorMessage = "Address is required.")]
    [StringLength(256, ErrorMessage = "Address must not exceed 256 characters.")]
    public string Address { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ward is required.")]
    [StringLength(100, ErrorMessage = "Ward must not exceed 100 characters.")]
    public string Ward { get; set; } = string.Empty;

    [Required(ErrorMessage = "District is required.")]
    [StringLength(100, ErrorMessage = "District must not exceed 100 characters.")]
    public string District { get; set; } = string.Empty;

    [Required(ErrorMessage = "City is required.")]
    [StringLength(100, ErrorMessage = "City must not exceed 100 characters.")]
    public string City { get; set; } = string.Empty;

    [Required(ErrorMessage = "Province is required.")]
    [StringLength(100, ErrorMessage = "Province must not exceed 100 characters.")]
    public string Province { get; set; } = string.Empty;

    [Required(ErrorMessage = "Postal code is required.")]
    [StringLength(20, ErrorMessage = "Postal code must not exceed 20 characters.")]
    [RegularExpression(
        pattern: CustomPattern.PostalCodePattern,
        ErrorMessage = "Invalid postal code.",
        MatchTimeoutInMilliseconds = 250
    )]
    public string PostalCode { get; set; } = string.Empty;


    public string? ProfileImage { get; set; }
}

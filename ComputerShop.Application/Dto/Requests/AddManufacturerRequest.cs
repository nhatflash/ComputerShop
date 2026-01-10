using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Application.Dto.Requests;

public class AddManufacturerRequest
{
    [Required(ErrorMessage = "Manufacturer name is required.")]
    [StringLength(256, ErrorMessage = "Manufacturer name must not exceed 256 characters.")]
    public string Name { get; set; } = string.Empty;
}

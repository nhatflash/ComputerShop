using System;
using System.ComponentModel.DataAnnotations;

namespace ComputerShop.Application.Dto.Requests;

public class AddCategoryRequest
{

    [Required(ErrorMessage = "Category name is required.")]
    [StringLength(256, ErrorMessage = "Category name must not exceed 256 characters.")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "Description is required.")]
    public string Description { get; set; } = string.Empty;

    public string? ImageUrl { get; set; }

}

using System;
using ComputerShop.Repository.CustomExceptions;
using ComputerShop.Repository.Models;
using ComputerShop.Repository.Repositories;

namespace ComputerShop.Service.Services;

public class CategoryService
{
    private readonly UnitOfWork _unitOfWork;

    public CategoryService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<Category> HandleAddCategory(string name, string description, string? imageUrl)
    {
        if (await _unitOfWork.CategoryRepository.CategoryExistByNameAsync(name))
        {
            throw new BadRequestException("This category name is already in use.");
        }

        var category = new Category
        {
            Id = Guid.NewGuid(),
            Name = name,
            Description = description,
            ImageUrl = imageUrl,
        };

        await _unitOfWork.CategoryRepository.AddCategoryAsync(category);

        return category;
    }
}

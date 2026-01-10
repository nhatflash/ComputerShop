using System;
using ComputerShop.Repository.CustomExceptions;
using ComputerShop.Repository.Enums;
using ComputerShop.Repository.Models;
using ComputerShop.Repository.Repositories;

namespace ComputerShop.Service.Services;

public class ProductService
{
    private readonly UnitOfWork _unitOfWork;

    public ProductService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }


    public async Task<Product> HandleAddProduct(Guid categoryId, Guid manufacturerId, string name, string description, Dictionary<string, string> specifications, decimal price)
    {
        if (!await _unitOfWork.CategoryRepository.CategoryExistByIdAsync(categoryId))
        {
            throw new NotFoundException("Category not found.");
        }

        if (!await _unitOfWork.ManufacturerRepository.ManufacturerExitByIdAsync(manufacturerId))
        {
            throw new NotFoundException("Manufacturer not found.");
        }

        if (await _unitOfWork.ProductRepository.ProductExistByNameAsync(name))
        {
            throw new BadRequestException("This product name is already in use.");
        }

        var product = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = categoryId,
            ManufacturerId = manufacturerId,
            Name = name,
            Description = description,
            Specifications = specifications,
            Price = price,
            Status = ProductStatus.Available,
        };

        await _unitOfWork.ProductRepository.AddProductAsync(product);

        return product;
    }

    public async Task<Product> FindProductById(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.FindProductByIdAndImagesAsync(id);
        if (product == null)
        {
            throw new NotFoundException("No product found.");
        }
        return product;
    }
}

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


    public async Task<Product> HandleAddProduct(Guid categoryId, Guid manufacturerId, string name, string description, Dictionary<string, string> specifications, decimal price, int warrantyMonth, List<string> imageUrls)
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

        if (price < 0)
        {
            throw new BadRequestException("Product price cannot be lower than 0.");
        }

        if (warrantyMonth < 0)
        {
            throw new BadRequestException("Warranty month cannot be lower than 0.");
        }

        var productId = Guid.NewGuid();
        List<ProductImage> productImages = [];
        foreach (var imageUrl in imageUrls)
        {
            var productImage = new ProductImage
            {
                ProductId = productId,
                ImageUrl = imageUrl,
            };
            productImages.Add(productImage);
        }

        var product = new Product
        {
            Id = productId,
            CategoryId = categoryId,
            ManufacturerId = manufacturerId,
            Name = name,
            Description = description,
            Specifications = specifications,
            Price = price,
            WarrantyMonth = warrantyMonth,
            Status = ProductStatus.Available,
            ProductImages = productImages,
        };

        await _unitOfWork.ProductRepository.AddProductAsync(product);

        return product;
    }

    public async Task<Product> FindProductById(Guid id)
    {
        var product = await _unitOfWork.ProductRepository.FindProductByIdIncludeImagesAsync(id) ?? throw new NotFoundException("No product found.");
        return product;
    }


    public async Task<Product> HandleUpdateProduct(Guid productId, string? name, string? description, Dictionary<string, string>? specifications, decimal? price, int? warrantyMonth)
    {
        var product = await _unitOfWork.ProductRepository.FindProductByIdIncludeImagesWithTrackingAsync(productId) ?? throw new BadRequestException("No product found.");
        return await UpdateNewProductInformation(product, name, description, specifications, price, warrantyMonth);
    }


    public async Task<Product> HandleAddProductQuantity(Guid productId, int quantity)
    {
        var product = await _unitOfWork.ProductRepository.FindProductByIdWithTrackingAsync(productId) ?? throw new NotFoundException("No product found.");

        if (quantity <= 0)
        {
            throw new BadRequestException("The requested quantity must be greater than 0.");
        }

        product.StockQuantity += quantity;
        await _unitOfWork.SaveChangesAsync();
        return product;
    }

    private async Task<Product> UpdateNewProductInformation(Product product, string? name, string? description, Dictionary<string, string>? specifications, decimal? price, int? warrantyMonth)
    {
        if (!string.IsNullOrWhiteSpace(name))
        {
            if (product.Name != name && await _unitOfWork.ProductRepository.ProductExistByNameAsync(name))
            {
                throw new BadRequestException("This product name is already in use.");
            }
            product.Name = name;
        }

        if (!string.IsNullOrWhiteSpace(description))
        {
            product.Description = description;
        }
        if (specifications != null && specifications.Count != 0)
        {
            product.Specifications = specifications;
        }
        if (price.HasValue)
        {
            if (price.Value < 0)
            {
                throw new BadRequestException("Price cannot be lower than 0.");
            }
            product.Price = price.Value;
        }
        if (warrantyMonth.HasValue)
        {
            if (warrantyMonth.Value < 0)
            {
                throw new BadRequestException("Warranty month cannot be lower than 0.");
            }
            product.WarrantyMonth = warrantyMonth.Value;
        }
        
        await _unitOfWork.SaveChangesAsync();
        return product;
    }



}

using ComputerShop.Application.Dto.Responses;
using ComputerShop.Repository.Models;

namespace ComputerShop.Application.Common;

public class ResponseMapper
{


    public static SignInResponse MapToSignInResponse(Dictionary<string, string> param)
    {
        return new SignInResponse
        {
            AccessToken = param["accessToken"],
            RefreshToken = param["refreshToken"]
        };
    }


    public static UserResponse MapToUserResponse(User user)
    {
        return new UserResponse
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Gender = user.Gender,
            Address = user.Address,
            Ward = user.Ward,
            District = user.District,
            City = user.City,
            Province = user.Province,
            PostalCode = user.PostalCode,
            ProfileImage = user.ProfileImage,
            VerifiedAt = user.VerifiedAt,
            CreatedAt = user.CreatedAt,
        };
    }

    public static List<UserResponse> MapToUserResponses(List<User> users)
    {
        List<UserResponse> responses = [];
        foreach (var user in users)
        {
            var response = MapToUserResponse(user);
            responses.Add(response);
        }
        return responses;
    }


    public static ManufacturerResponse MapToManufacturerResponse(Manufacturer manufacturer)
    {
        return new ManufacturerResponse
        {
            Id = manufacturer.Id,
            Name = manufacturer.Name,
        };
    }

    public static List<ManufacturerResponse> MapToManufacturerResponses(List<Manufacturer> manufacturers)
    {
        List<ManufacturerResponse> responses = [];
        foreach (var manufacturer in manufacturers)
        {
            var response = MapToManufacturerResponse(manufacturer);
            responses.Add(response);
        }
        return responses;
    }

    public static CategoryResponse MapToCategoryResponse(Category category)
    {
        return new CategoryResponse
        {
            Id = category.Id,
            Name = category.Name,
            Description = category.Description,
            ImageUrl = category.ImageUrl,
        };
    }

    public static List<CategoryResponse> MapToCategoryResponses(List<Category> categories)
    {
        List<CategoryResponse> responses = [];
        foreach (var category in categories)
        {
            var response = MapToCategoryResponse(category);
            responses.Add(response);
        }
        return responses;
    }


    public static ProductResponse MapToProductResponse(Product product)
    {
        return new ProductResponse
        {
            Id = product.Id,
            CategoryId = product.CategoryId,
            ManufacturerId = product.ManufacturerId,
            Name = product.Name,
            Description = product.Description,
            Specifications = product.Specifications,
            Price = product.Price,
            StockQuantity = product.StockQuantity,
            Status = product.Status
        };
    }

    public static List<ProductResponse> MapToProductResponses(List<Product> products)
    {
        List<ProductResponse> responses = [];
        foreach (var product in products)
        {
            var response = MapToProductResponse(product);
            responses.Add(response);
        }
        return responses;
    }

    public static ProductImageResponse MapToProductImageResponse(ProductImage productImage)
    {
        return new ProductImageResponse
        {
            Id = productImage.Id,
            ProductId = productImage.ProductId,
            ImageUrl = productImage.ImageUrl,
        };
    }


    public static List<ProductImageResponse> MapToProductImageResponses(List<ProductImage> productImages)
    {
        List<ProductImageResponse> responses = [];
        foreach (var productImage in productImages)
        {
            var response = MapToProductImageResponse(productImage);
            responses.Add(response);
        }
        return responses;
    }


    public static OrderItemResponse MapToOrderItemResponse(OrderItem orderItem)
    {
        return new OrderItemResponse
        {
            Id = orderItem.Id,
            OrderId = orderItem.OrderId,
            ProductId = orderItem.ProductId,
            Quantity = orderItem.Quantity,
            UnitPrice = orderItem.UnitPrice,
            SubTotal = orderItem.SubTotal
        };
    }

    public static List<OrderItemResponse> MapToOrderItemResponses(List<OrderItem> orderItems)
    {
        List<OrderItemResponse> responses = [];
        foreach (var orderItem in orderItems)
        {
            var response = MapToOrderItemResponse(orderItem);
            responses.Add(response);
        }
        return responses;
    }

    public static OrderResponse MapToOrderResponse(Order order)
    {
        return new OrderResponse
        {
            Id = order.Id,
            UserId = order.UserId,
            OrderDate = order.OrderDate,
            SubTotal = order.SubTotal,
            ShippingCost = order.ShippingCost,
            Tax = order.Tax,
            TotalAmount = order.TotalAmount,
            Type = order.Type,
            ShippingAddress = order.ShippingAddress,
            TrackingPhone = order.TrackingPhone,
            Status = order.Status,
            Notes = order.Notes,
            OrderItems = MapToOrderItemResponses((List<OrderItem>)order.OrderItems)
        };
    }

    public static List<OrderResponse> MapToOrderResponses(List<Order> orders)
    {
        List<OrderResponse> responses = [];
        foreach (var order in orders)
        {
            var response = MapToOrderResponse(order);
            responses.Add(response);
        }
        return responses;
    }
}

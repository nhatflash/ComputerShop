using System;
using ComputerShop.Repository.Models;

namespace ComputerShop.Service.Services;

public class ServiceProviders
{
    public AuthService AuthService { get; }
    public UserService UserService { get; }
    public ManufacturerService ManufacturerService { get; }
    public CategoryService CategoryService { get; }
    public ProductService ProductService { get; }

    public ServiceProviders(AuthService authService, 
                            UserService userService, 
                            ManufacturerService manufacturerService, 
                            CategoryService categoryService, 
                            ProductService productService)
    {
        AuthService = authService;
        UserService = userService;
        ManufacturerService = manufacturerService;
        CategoryService = categoryService;
        ProductService = productService;
    }
}

using System;

namespace ComputerShop.Service.Services;

public class ServiceProviders
{
    public AuthService AuthService { get; }
    public UserService UserService { get; }

    public ServiceProviders(AuthService authService, UserService userService)
    {
        AuthService = authService;
        UserService = userService;
    }
}

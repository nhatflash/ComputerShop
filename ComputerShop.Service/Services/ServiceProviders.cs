using System;

namespace ComputerShop.Service.Services;

public class ServiceProviders
{
    public AuthService AuthService { get; }

    public ServiceProviders(AuthService authService)
    {
        AuthService = authService;
    }
}

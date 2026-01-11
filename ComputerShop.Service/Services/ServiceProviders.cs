namespace ComputerShop.Service.Services;

public class ServiceProviders
{
    public AuthService AuthService { get; }
    public UserService UserService { get; }
    public ManufacturerService ManufacturerService { get; }
    public CategoryService CategoryService { get; }
    public ProductService ProductService { get; }
    public OrderService OrderService { get; set; }
    public PaymentService PaymentService { get; set; }

    public ServiceProviders(AuthService authService, 
                            UserService userService, 
                            ManufacturerService manufacturerService, 
                            CategoryService categoryService, 
                            ProductService productService, 
                            OrderService orderService, 
                            PaymentService paymentService)
    {
        AuthService = authService;
        UserService = userService;
        ManufacturerService = manufacturerService;
        CategoryService = categoryService;
        ProductService = productService;
        OrderService = orderService;
        PaymentService = paymentService;
    }
}

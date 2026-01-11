namespace ComputerShop.Repository.Models;

public class Manufacturer
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public DateTimeOffset CreatedAt { get; } = DateTimeOffset.Now;
}

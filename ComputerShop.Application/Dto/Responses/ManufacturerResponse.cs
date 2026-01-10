using System;

namespace ComputerShop.Application.Dto.Responses;

public class ManufacturerResponse
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;
}

using System;

namespace ComputerShop.Application.Common;

public class ApiResponse<T>
{
    public int StatusCode { get; set; }

    public T Value { get; set; } = default!;
}

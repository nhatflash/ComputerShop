using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace ComputerShop.Service.Context;

public class UserContext
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserContext(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public Guid GetCurrentAuthenticatedUserId()
    {
        string? idStr = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (idStr == null) {
            throw new UnauthorizedAccessException();
        }
        return Guid.Parse(idStr);
    }

}

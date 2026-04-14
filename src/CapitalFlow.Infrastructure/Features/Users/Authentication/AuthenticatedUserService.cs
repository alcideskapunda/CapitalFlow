using CapitalFlow.Application.Features.Users.Auth;
using CapitalFlow.Domain.Entities.Users;
using CapitalFlow.Persistence.Interceptors;
using Microsoft.AspNetCore.Http;

namespace CapitalFlow.Infrastructure.Features.Users.Authentication;

public class AuthenticatedUserService : IAuthenticatedUserService, IAuditingInformationService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public AuthenticatedUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private string? GetClaimValue(string claimType)
    {
        return _httpContextAccessor.HttpContext?.User.FindFirst(claimType)?.Value;
    }

    public Guid? CustomerId()
    {
        var claim = GetClaimValue("CustomerId");
        return Guid.TryParse(claim, out var guid) ? guid : null;
    }

    public Guid? GetUserId()
    {
        var claim = GetClaimValue("UserId");
        return Guid.TryParse(claim, out var guid) ? guid : null;
    }

    public bool IsAdmin()
    {
        return _httpContextAccessor.HttpContext?.User.IsInRole(nameof(UserType.Admin)) == true;
    }

    public bool IsAuthenticated()
    {
        return _httpContextAccessor.HttpContext?.User.Identity?.IsAuthenticated ?? false;
    }

    public bool IsCustomer()
    {
        return _httpContextAccessor.HttpContext?.User.IsInRole(nameof(UserType.Customer)) == true;
    }
}

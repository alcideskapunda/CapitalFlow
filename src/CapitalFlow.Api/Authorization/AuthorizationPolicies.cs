using CapitalFlow.Domain.Entities.Users;
using Microsoft.AspNetCore.Authorization;

namespace CapitalFlow.Api.Authorization;

public static class AuthorizationPolicies
{
    public const string AdminsOnlyPolicy = "AdminsOnly";
    public const string CustomersOnlyPolicy = "CustomersOnly";

    public static void AdminsOnlyPolicyConfiguration(AuthorizationPolicyBuilder builder)
    {
        builder.RequireRole(nameof(UserType.Admin));
    }

    public static void CustomersOnlyPolicyConfiguration(AuthorizationPolicyBuilder builder)
    {
        builder.RequireRole(nameof(UserType.Customer));
    }
}

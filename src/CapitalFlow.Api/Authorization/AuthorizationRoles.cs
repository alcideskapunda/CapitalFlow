using CapitalFlow.Domain.Entities.Users;

namespace CapitalFlow.Api.Authorization;

public static class AuthorizationRoles
{
    public static string Admin => nameof(UserType.Admin);
    public static string Customer => nameof(UserType.Customer);
}

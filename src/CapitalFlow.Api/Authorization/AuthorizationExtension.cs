namespace CapitalFlow.Api.Authorization;

public static class AuthorizationExtension
{
    public static void AddCapitalFlowAuthorization(this IServiceCollection services)
    {
        services.AddAuthorization(options =>
        {
            options.AddPolicy(AuthorizationPolicies.AdminsOnlyPolicy, AuthorizationPolicies.AdminsOnlyPolicyConfiguration);
            options.AddPolicy(AuthorizationPolicies.CustomersOnlyPolicy, AuthorizationPolicies.CustomersOnlyPolicyConfiguration);
        });
    }
}

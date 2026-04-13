namespace CapitalFlow.Application.Features.Users.Auth;

public interface IAuthenticatedUserService
{
    Guid? GetUserId();
    Guid? CustomerId();
    bool IsCustomer();
    bool IsAdmin();
    bool IsAuthenticated();
}

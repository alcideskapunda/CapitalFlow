namespace CapitalFlow.Application.Features.Users.Auth;

public interface IJwtService
{
    Task<string> GenerateUserTokenAsync(Guid userId, CancellationToken ct = default);
}

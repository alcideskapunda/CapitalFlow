using CapitalFlow.Application.Features.Users.Auth;
using CapitalFlow.Domain.Entities.Users;
using CapitalFlow.Persistence.Database;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CapitalFlow.Infrastructure.Features.Users.Authentication;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;
    private readonly CapitalFlowDbContext _database;

    public JwtService(IConfiguration configuration, CapitalFlowDbContext database)
    {
        _configuration = configuration;
        _database = database;
    }

    private string SigningKey => _configuration["Jwt:SigningKey"]!;

    public async Task<string> GenerateUserTokenAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await _database.Users
            .AsNoTracking()
            .Where(x => x.Id == userId)
            .Select(u => new { u.Id, u.Email, u.AccessLevel, u.Type })
            .FirstAsync(ct);

        var additionalClaims = new List<(string, string)>();
        string role;

        switch (user.Type)
        {
            case UserType.Customer:
                role = nameof(UserType.Customer);

                var customerId = await _database.Customers
                    .Where(c => c.UserId == userId)
                    .Select(c => c.Id)
                    .FirstAsync(ct);

                additionalClaims.Add(("CustomerId", customerId.ToString()));
                break;

            case UserType.Admin:
                role = nameof(UserType.Admin);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return BuildJwtToken(
            userId: user.Id.ToString(),
            email: user.Email,
            accessLevel: user.AccessLevel,
            role: role,
            extraClaims: additionalClaims
        );
    }

    private string BuildJwtToken(string userId, string email, int accessLevel, string role, IEnumerable<(string Type, string Value)> extraClaims)
    {
        return JwtBearer.CreateToken(options =>
        {
            options.SigningKey = SigningKey;
            options.ExpireAt = DateTime.Now.AddDays(1);
            options.User.Claims.Add(("UserId", userId));
            options.User.Claims.Add(("UserEmail", email));
            options.User.Claims.Add(("AccessLevel", ((int)accessLevel).ToString()));

            foreach (var claim in extraClaims)
            {
                options.User.Claims.Add(claim);
            }

            if (!string.IsNullOrWhiteSpace(role))
            {
                options.User.Roles.Add(role);
            }
        });
    }
}

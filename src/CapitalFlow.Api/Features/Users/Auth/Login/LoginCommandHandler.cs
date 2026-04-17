using CapitalFlow.Application.Features.Users.Auth;
using CapitalFlow.Persistence.Database;
using ErrorOr;
using Microsoft.EntityFrameworkCore;

namespace CapitalFlow.Api.Features.Users.Auth.Login;

public sealed class LoginCommandHandler : ICommandHandler<LoginCommand, ErrorOr<string>>
{
    private readonly CapitalFlowDbContext _database;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtService _jwtService;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(CapitalFlowDbContext database, IPasswordHasher passwordHasher, IJwtService jwtService, ILogger<LoginCommandHandler> logger)
    {
        _database = database;
        _passwordHasher = passwordHasher;
        _jwtService = jwtService;
        _logger = logger;
    }

    public async Task<ErrorOr<string>> ExecuteAsync(LoginCommand request, CancellationToken ct)
    {
        var user = await _database.Users.FirstOrDefaultAsync(u => u.Email == request.Email, ct);
        
        if (user is null || !_passwordHasher.VerifyPassword(request.Password, user.Password))
        {
            _logger.LogWarning("Invalid credentials for {Email}", request.Email);
            return Error.Validation("Invalid credentials");
        }

        return await _jwtService.GenerateUserTokenAsync(user.Id, ct);
    }
}
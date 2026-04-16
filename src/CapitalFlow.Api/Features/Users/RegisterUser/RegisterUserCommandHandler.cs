using CapitalFlow.Application.Features.Users.Auth;
using CapitalFlow.Domain.Entities.Users;
using CapitalFlow.Persistence.Database;
using ErrorOr;

namespace CapitalFlow.Api.Features.Users.RegisterUser;

public sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand, ErrorOr<User>>
{
    private readonly ILogger<RegisterUserCommandHandler> _logger;
    private readonly CapitalFlowDbContext _database;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(ILogger<RegisterUserCommandHandler> logger, CapitalFlowDbContext database, IPasswordHasher passwordHasher, IUserRepository userRepository)
    {
        _logger = logger;
        _database = database;
        _passwordHasher = passwordHasher;
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<User>> ExecuteAsync(RegisterUserCommand request, CancellationToken ct)
    {
        bool emailExists = await _userRepository.EmailExistsAsync(request.Email, ct);

        if (emailExists)
        {
            return Error.Conflict("Email already exists");
        }

        string password = _passwordHasher.HashPassword(request.Password);

        var user = await User.CreateAsync(
            name: request.Name,
            email: request.Email,
            password: password,
            type: request.Type,
            userRepository: _userRepository,
            ct: ct
        );

        if (user.IsError)
        {
            return user.Errors;
        }

        await _database.Users.AddAsync(user.Value, ct);
        await _database.SaveChangesAsync(ct);

        _logger.LogInformation("User registered successfully: {UserId}", user.Value.Id);

        return user;
    }
}

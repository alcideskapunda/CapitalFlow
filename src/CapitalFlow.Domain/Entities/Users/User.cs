using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Domain.Entities.Customers.Customers;
using ErrorOr;

namespace CapitalFlow.Domain.Entities.Users;

public sealed class User : Entity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserType Type { get; set; }
    public int AccessLevel { get; set; }
    public bool EmailVerified { get; set; }
    public bool ResetPassword { get; set; }
    public Customer? Customer { get; set; }

    public static async Task<ErrorOr<User>> CreateAsync(
        string name, string email, string password, UserType type, IUserRepository userRepository, CancellationToken ct = default)
    {
        if (await userRepository.EmailExistsAsync(email, ct))
        {
            return Error.Conflict("Email already exists");
        }

        return new User
        {
            Name = name,
            Email = email,
            Password = password,
            EmailVerified = true,
            Type = type,
            AccessLevel = type == UserType.Admin ? 1 : 0
        };
    }
}

using ErrorOr;
using CapitalFlow.Domain.Entities.Users;

namespace CapitalFlow.Api.Features.Users.RegisterUser;

public record RegisterUserCommand : ICommand<ErrorOr<User>>
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public UserType Type { get; set; }
}

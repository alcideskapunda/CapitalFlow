using ErrorOr;

namespace CapitalFlow.Api.Features.Users.Auth.Login;

public record LoginCommand(string Email, string Password) : ICommand<ErrorOr<string>>;
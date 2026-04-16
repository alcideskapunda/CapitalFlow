using FluentValidation;

namespace CapitalFlow.Api.Features.Users.RegisterUser;

public sealed class RegisterUserCommandValidator : Validator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(x => x.Name).NotEmpty().MinimumLength(5);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}

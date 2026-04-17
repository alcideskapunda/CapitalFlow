using System.Net.Mail;
using FluentValidation;

namespace CapitalFlow.Api.Features.Users.Auth.Login;

public sealed class LoginCommandValidator : Validator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().Must(x => IsValidEmail(x)).WithMessage("Email invalid");
        RuleFor(x => x.Password).NotEmpty();
    }
    private static bool IsValidEmail(string input) =>  MailAddress.TryCreate(input, out _);
}
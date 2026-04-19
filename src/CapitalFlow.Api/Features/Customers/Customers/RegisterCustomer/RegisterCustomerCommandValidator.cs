using FluentValidation;

namespace CapitalFlow.Api.Features.Customers.Customers.RegisterCustomer;

public sealed class RegisterCustomerCommandValidator : Validator<RegisterCustomerCommand>
{
    public  RegisterCustomerCommandValidator()
    {
        RuleFor(p => p.Name).NotEmpty().MinimumLength(3);
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
        RuleFor(x => x.MonthlyAmount).NotEqual(0);
        RuleFor(x => x.Cpf)
            .NotEmpty()
            .Matches(@"^\d{11}$")
            .WithMessage("CPF must contain 11 digits.");
    }
}
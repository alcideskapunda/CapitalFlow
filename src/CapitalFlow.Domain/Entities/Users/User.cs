using CapitalFlow.Domain.Entities.Common;
using CapitalFlow.Domain.Entities.Customers.Customers;

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
}

namespace CapitalFlow.Api.Features.Users.Common;

public record UserViewModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public bool EmailConfirmed { get; set; }
    public string Role { get; set; } = null!;
    public bool ResetPassword { get; set; }
}

using CapitalFlow.Domain.Entities.Users;

namespace CapitalFlow.Api.Features.Users.Common;

public static class Extensions
{
    public static IQueryable<UserViewModel> ProjectToViewModel(this IQueryable<User> users)
    {
        return users.Select(u => new UserViewModel
        {
            Id = u.Id,
            Name = u.Name,
            Email = u.Email,
            EmailConfirmed = u.EmailVerified,
            Role = u.Type.ToString(),
            ResetPassword = u.ResetPassword,
        });
    }
}

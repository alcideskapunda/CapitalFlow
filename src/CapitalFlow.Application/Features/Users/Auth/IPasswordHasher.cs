namespace CapitalFlow.Application.Features.Users.Auth;

public interface IPasswordHasher
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hash);
}

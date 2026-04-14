using System.Security.Cryptography;
using System.Text;
using CapitalFlow.Application.Features.Users.Auth;

namespace CapitalFlow.Infrastructure.Features.Users.Authentication;

public class PasswordHasher : IPasswordHasher
{
    private const int SaltLength = 6;
    private const string SaltDelimiter = "*";

    private string GenerateRandomSalt()
    {
        var randomSaltBytes = RandomNumberGenerator.GetBytes(SaltLength);
        string salt = Convert.ToBase64String(randomSaltBytes);
        return salt;
    }

    private string HashString(string input)
    {
        SHA512 hashAlgorithm = SHA512.Create();
        return Convert.ToBase64String(hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input)));
    }

    private string HashWithSalt(string password, string salt)
    {
        return HashString(salt + password);
    }

    public string HashPassword(string password)
    {
        var salt = GenerateRandomSalt();
        var hash = HashWithSalt(password, salt);
        var finalHash = salt + SaltDelimiter + hash;
        return finalHash;
    }

    public bool VerifyPassword(string password, string passwordHash)
    {
        var parts = passwordHash.Split(SaltDelimiter);

        if (parts.Length != 2)
        {
            return false;
        }

        var salt = parts[0];
        var hash = HashWithSalt(password, salt);
        return hash == parts[1];
    }
}

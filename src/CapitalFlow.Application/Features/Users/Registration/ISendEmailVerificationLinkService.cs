namespace CapitalFlow.Application.Features.Users.Registration;

public interface ISendEmailVerificationLinkService
{
    Task SendEmailVerificationLinkAsync(Guid userId, CancellationToken ct = default);
    public void SendEmailVerificationLink(Guid userId, string userEmail, string token, CancellationToken ct = default);
}

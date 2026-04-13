namespace CapitalFlow.Application.Features.Emails;

public interface ISendEmailService
{
    void SendEmail(Email email);
}

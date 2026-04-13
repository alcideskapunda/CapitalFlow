namespace CapitalFlow.Application.Features.Emails;

public record struct Email
{
    public string To { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
    public bool IsHtml { get; set; }
}

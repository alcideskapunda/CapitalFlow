namespace CapitalFlow.Infrastructure.Configuration.Hangfire;

public struct HangFireSecrets
{
    public string Login { get; set; }
    public string Password { get; set; }
    public string RouteName { get; set; }
}

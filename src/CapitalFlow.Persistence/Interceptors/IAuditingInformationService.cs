namespace CapitalFlow.Persistence.Interceptors;

public interface IAuditingInformationService
{
    Guid? GetUserId();
}

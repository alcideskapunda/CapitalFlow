namespace CapitalFlow.Domain.Entities.Common;

public abstract class Entity
{
    public Guid Id { get; set; } = Guid.NewGuid();
}

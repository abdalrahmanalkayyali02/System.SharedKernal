using SharedKernal.Abstract.Messaging;

namespace SharedKernal.Abstract.Entity;

public abstract class AggregateRoot : AuditLogEntity
{
    private readonly List<IDomainEvent> _domainEvents = new();
        
    // Expose as ReadOnly so the Application Layer can't bypass the Raise method
    public IReadOnlyCollection<IDomainEvent> GetDomainEvents() => _domainEvents.AsReadOnly();

    public void ClearDomainEvents() => _domainEvents.Clear();

    // Only the Aggregate (and its children) can record events
    protected void RaiseDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    } 
}
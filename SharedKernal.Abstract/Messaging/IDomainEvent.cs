namespace SharedKernal.Abstract.Messaging;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}
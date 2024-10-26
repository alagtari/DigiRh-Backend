namespace Conge.Domain.Events;

public record LeaveCreatedEvent(Leave leave) : IDomainEvent;

namespace Conge.Domain.Events;

public record LeaveSubmittedEvent(Guid UserId, Guid LeaveId) : IDomainEvent;
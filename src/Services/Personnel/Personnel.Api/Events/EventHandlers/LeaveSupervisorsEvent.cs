namespace Personnel.Api.Events.EventHandlers;

public class LeaveSupervisorsEvent : INotification
{
    Guid EventId => Guid.NewGuid();
    public DateTime OccurredOn => DateTime.Now;
    public string EventType => GetType().AssemblyQualifiedName;
    
}


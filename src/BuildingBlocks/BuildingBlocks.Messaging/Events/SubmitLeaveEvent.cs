namespace BuildingBlocks.Messaging.Events;

public record SubmitLeaveEvent : IntegrationEvent
{
    public Guid LeaveId { get; set; }
    public Guid UserId { get; set; }
}
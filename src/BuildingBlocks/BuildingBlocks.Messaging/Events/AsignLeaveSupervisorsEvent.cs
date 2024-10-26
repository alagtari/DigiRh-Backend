using Personnel.Api.Dtos;

namespace BuildingBlocks.Messaging.Events;

public record AsignLeaveSupervisorsEvent : IntegrationEvent
{
    public List<SupervisorDto> Supervisors { get; set; }
    public Guid LeaveId { get; set; }
}
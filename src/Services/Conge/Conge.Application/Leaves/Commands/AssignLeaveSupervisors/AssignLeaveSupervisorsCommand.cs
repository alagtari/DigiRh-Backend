using Personnel.Api.Dtos;

namespace Conge.Application.Leaves.Commands.AssignLeaveSupervisors;

public record AssignLeaveSupervisorsCommand(
    List<SupervisorDto> Supervisors,
    Guid LeaveId)
    : ICommand<AssignLeaveSupervisorsResult>;

public record AssignLeaveSupervisorsResult(bool Success);
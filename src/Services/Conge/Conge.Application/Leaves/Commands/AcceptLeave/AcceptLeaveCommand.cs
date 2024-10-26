namespace Conge.Application.Leaves.Commands.AcceptLeave;

public record AcceptLeaveCommand(Guid LeaveId, Guid UserId)
    : ICommand<AcceptLeaveResult>;

public record AcceptLeaveResult(bool Success);
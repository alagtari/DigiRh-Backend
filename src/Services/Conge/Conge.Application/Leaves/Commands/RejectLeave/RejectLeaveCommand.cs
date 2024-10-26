namespace Conge.Application.Leaves.Commands.RejectLeave;

public record RejectLeaveCommand(Guid LeaveId, Guid UserId)
    : ICommand<RejectLeaveResult>;

public record RejectLeaveResult(bool Success);
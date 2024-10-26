namespace Conge.Application.Leaves.Commands.UpdateLeave;

public record UpdateLeaveCommand(LeaveUpdateDto Leave)
    : ICommand<UpdateLeaveResult>;

public record UpdateLeaveResult(bool Success);
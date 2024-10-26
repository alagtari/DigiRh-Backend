namespace Conge.Application.Leaves.Commands.SubmitLeave;

public record SubmitLeaveCommand(Guid LeaveId)
    : ICommand<SubmitLeaveResult>;

public record SubmitLeaveResult(LeaveDto Payload);
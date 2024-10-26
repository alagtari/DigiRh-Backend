namespace Conge.Application.Leaves.Commands.DeleteLeave;

public record DeleteLeaveCommand(Guid Id)
    : ICommand<DeleteLeaveResult>;
public record DeleteLeaveResult(bool Success);
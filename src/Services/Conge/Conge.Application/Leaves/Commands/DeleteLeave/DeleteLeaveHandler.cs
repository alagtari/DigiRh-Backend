using Conge.Application.Exceptions;
using Conge.Domain.ValueObjects;

namespace Conge.Application.Leaves.Commands.DeleteLeave;

public class DeleteLeaveHandler(IApplicationDbContext dbContext)
    : ICommandHandler<DeleteLeaveCommand, DeleteLeaveResult>
{
    public async Task<DeleteLeaveResult> Handle(DeleteLeaveCommand command, CancellationToken cancellationToken)
    {
        var leaveId = LeaveId.Of(command.Id);
        var leave = await dbContext.Leaves
            .FindAsync([leaveId], cancellationToken: cancellationToken);

        if (leave is null)
        {
            throw new LeaveNotFoundException(command.Id);
        }

        dbContext.Leaves.Remove(leave);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new DeleteLeaveResult(true);
    }
}
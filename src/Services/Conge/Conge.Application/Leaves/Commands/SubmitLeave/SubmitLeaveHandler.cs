using Conge.Application.Exceptions;
using Conge.Domain.ValueObjects;

namespace Conge.Application.Leaves.Commands.SubmitLeave;

public class SubmitLeaveHandler(IApplicationDbContext dbContext)
    : ICommandHandler<SubmitLeaveCommand, SubmitLeaveResult>
{
    public async Task<SubmitLeaveResult> Handle(SubmitLeaveCommand command, CancellationToken cancellationToken)
    {
        var leaveId = LeaveId.Of(command.LeaveId);
        var leave = await dbContext.Leaves
            .FindAsync([leaveId], cancellationToken: cancellationToken);

        if (leave is null)
        {
            throw new LeaveNotFoundException(command.LeaveId);
        }

        leave.Update(null, null, null, null, null, LeaveStatus.Pending
        );

        dbContext.Leaves.Update(leave);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new SubmitLeaveResult(leave.ToLeaveDto());
    }
}
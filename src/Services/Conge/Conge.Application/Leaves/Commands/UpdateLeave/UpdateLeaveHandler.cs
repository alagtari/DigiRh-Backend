using Conge.Application.Exceptions;
using Conge.Domain.ValueObjects;

namespace Conge.Application.Leaves.Commands.UpdateLeave;

public class UpdateLeaveHandler(IApplicationDbContext dbContext)
    : ICommandHandler<UpdateLeaveCommand, UpdateLeaveResult>
{
    public async Task<UpdateLeaveResult> Handle(UpdateLeaveCommand command, CancellationToken cancellationToken)
    {
        var leaveId = LeaveId.Of(command.Leave.LeaveId);
        var leave = await dbContext.Leaves
            .FindAsync([leaveId], cancellationToken: cancellationToken);

        if (leave is null)
        {
            throw new LeaveNotFoundException(command.Leave.LeaveId);
        }

        leave.Update(
            command.Leave.LeaveType, command.Leave.StartDate, command.Leave.EndDate,
            command.Leave.Phone, command.Leave.Pay, command.Leave.Status
        );

        dbContext.Leaves.Update(leave);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new UpdateLeaveResult(true);
    }
}
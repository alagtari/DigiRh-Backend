using Conge.Application.Exceptions;
using Conge.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Conge.Application.Leaves.Commands.RejectLeave;

public class RejectLeaveHandler(IApplicationDbContext dbContext)
    : ICommandHandler<RejectLeaveCommand, RejectLeaveResult>
{
    public async Task<RejectLeaveResult> Handle(RejectLeaveCommand command, CancellationToken cancellationToken)
    {
        var leaveId = LeaveId.Of(command.LeaveId);


        var leave = await dbContext.Leaves
            .FindAsync(new object[] { leaveId }, cancellationToken);
        if (leave == null)
        {
            throw new NotFoundException($"Leave with ID {command.LeaveId} not found.");
        }

        var supervisors = await dbContext.Supervisors
            .Where(s => s.LeaveId == leaveId)
            .ToListAsync(cancellationToken);
        if (supervisors == null || !supervisors.Any())
        {
            throw new NotFoundException($"No supervisors found for leave ID {command.LeaveId}.");
        }

        var supervisor = supervisors.SingleOrDefault(s => s.PersonnelId == command.UserId);
        if (supervisor == null)
        {
            throw new NotFoundException(
                $"Supervisor with user ID {command.UserId} not found for leave ID {command.LeaveId}.");
        }

        supervisor.Update(LeaveAgreement.Rejected);
        leave.Update(null, null, null, null, null, LeaveStatus.Rejected);


        await dbContext.SaveChangesAsync(cancellationToken);

        return new RejectLeaveResult(true);
    }
}
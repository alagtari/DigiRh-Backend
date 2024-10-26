using Conge.Application.Exceptions;
using Conge.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Conge.Application.Leaves.Commands.AcceptLeave;

public class AcceptLeaveHandler(IApplicationDbContext dbContext)
    : ICommandHandler<AcceptLeaveCommand, AcceptLeaveResult>
{
    public async Task<AcceptLeaveResult> Handle(AcceptLeaveCommand command, CancellationToken cancellationToken)
    {
        var leaveId = LeaveId.Of(command.LeaveId);

        // Retrieve leave request
        var leave = await dbContext.Leaves
            .FindAsync(new object[] { leaveId }, cancellationToken);
        if (leave == null)
        {
            throw new NotFoundException($"Leave with ID {command.LeaveId} not found.");
        }

        // Retrieve all supervisors for this leave
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

        supervisor.Update(LeaveAgreement.Accepted);

        if (supervisors.All(s => s.Accord == LeaveAgreement.Accepted))
        {
            leave.Update(null, null, null, null, null, LeaveStatus.Accepted);
        }

        await dbContext.SaveChangesAsync(cancellationToken);

        return new AcceptLeaveResult(true);
    }
}
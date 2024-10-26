using Conge.Application.Exceptions;
using Conge.Domain.Models;
using Conge.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Conge.Application.Leaves.Commands.AssignLeaveSupervisors;

public class AssignLeaveSupervisorsHandler(
    IApplicationDbContext dbContext,
    ILogger<AssignLeaveSupervisorsHandler> logger)
    : ICommandHandler<AssignLeaveSupervisorsCommand, AssignLeaveSupervisorsResult>
{
    public async Task<AssignLeaveSupervisorsResult> Handle(AssignLeaveSupervisorsCommand command,
        CancellationToken cancellationToken)
    {
        var firstSpervisor = Supervisor.Create(command.Supervisors[0].Id,
            command.Supervisors[0].Prenom, command.Supervisors[0].Nom, command.Supervisors[0].Image,
            LeaveId.Of(command.LeaveId));
        var secondSpervisor = Supervisor.Create(command.Supervisors[1].Id,
            command.Supervisors[1].Prenom, command.Supervisors[1].Nom, command.Supervisors[1].Image,
            LeaveId.Of(command.LeaveId));

        var leaveId = LeaveId.Of(command.LeaveId);

        var leave = await dbContext.Leaves
            .FindAsync([leaveId], cancellationToken: cancellationToken);


        if (leave is null)
        {
            throw new LeaveNotFoundException(command.LeaveId);
        }

        leave.AddSupervisor(firstSpervisor);
        leave.AddSupervisor(secondSpervisor);


        dbContext.Leaves.Update(leave);
        await dbContext.SaveChangesAsync(cancellationToken);

        return new AssignLeaveSupervisorsResult(true);
    }
}
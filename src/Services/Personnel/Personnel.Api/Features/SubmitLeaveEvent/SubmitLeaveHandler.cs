using BuildingBlocks.Messaging.Events;
using MassTransit;
using Personnel.Api.Dtos;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

public record SubmitLeaveCommand(Guid UserId, Guid LeaveId) : ICommand<SubmitLeaveResult>;

public record SubmitLeaveResult();

public class SubmitLeaveHandler(
    ApplicationDbContext dbContext,
    IPublishEndpoint publishEndpoint,
    ILogger<SubmitLeaveHandler> logger)
    : ICommandHandler<SubmitLeaveCommand, SubmitLeaveResult>
{
    public async Task<SubmitLeaveResult> Handle(SubmitLeaveCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling leave submission for UserId: {UserId}", command.UserId);
        
        var personnel = await dbContext.Persons
            .Where(p => p.Id == command.UserId)
            .Select(p => new 
            {
                p.Id,
                Supervisor = new 
                {
                    p.Supervisor.Id,
                    p.Supervisor.Prenom,
                    p.Supervisor.Nom,
                    p.Supervisor.Image,
                    p.Supervisor.SupervisorId
                }
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (personnel == null)
        {
            logger.LogWarning("Personnel not found for UserId: {UserId}", command.UserId);
            return new SubmitLeaveResult();
        }

        var firstSupervisor = personnel.Supervisor;
        if (firstSupervisor == null)
        {
            logger.LogWarning("First supervisor not found for PersonnelId: {PersonnelId}", personnel.Id);
            return new SubmitLeaveResult();
        }

        logger.LogInformation("First supervisor found for PersonnelId: {PersonnelId}, SupervisorId: {SupervisorId}", personnel.Id, firstSupervisor.Id);

        var secondSupervisor = await dbContext.Persons
            .Where(p => p.Id == firstSupervisor.SupervisorId)
            .Select(p => new
            {
                p.Id,
                p.Prenom,
                p.Nom,
                p.Image
            })
            .FirstOrDefaultAsync(cancellationToken);

        if (secondSupervisor != null)
        {
            logger.LogInformation("Second supervisor found for SupervisorId: {SupervisorId}", secondSupervisor.Id);
        }
        else
        {
            logger.LogWarning("Second supervisor not found for first SupervisorId: {SupervisorId}", firstSupervisor.Id);
        }

        // Create supervisors list
        var supervisors = new List<SupervisorDto>
        {
            new SupervisorDto
            {
                Id = firstSupervisor.Id,
                Prenom = firstSupervisor.Prenom,
                Nom = firstSupervisor.Nom,
                Image = firstSupervisor.Image,
            }
        };

        if (secondSupervisor != null)
        {
            supervisors.Add(new SupervisorDto
            {
                Id = secondSupervisor.Id,
                Prenom = secondSupervisor.Prenom,
                Nom = secondSupervisor.Nom,
                Image = secondSupervisor.Image,
            });
        }

        var assignLeaveSupervisorsEvent = new AsignLeaveSupervisorsEvent
        {
            Supervisors = supervisors,
            LeaveId = command.LeaveId,
        };

        await publishEndpoint.Publish(assignLeaveSupervisorsEvent, cancellationToken);

        return new SubmitLeaveResult();
    }
}

using BuildingBlocks.Messaging.Events;
using Conge.Application.Leaves.Commands.AssignLeaveSupervisors;
using Microsoft.Extensions.Logging;

namespace Conge.Application.Leaves.EventHandlers.Integration;

public class AsignLeaveSupervisorsEventHsndler(ISender sender, ILogger<AsignLeaveSupervisorsEventHsndler> logger)
    : IConsumer<AsignLeaveSupervisorsEvent>
{
    public async Task Consume(ConsumeContext<AsignLeaveSupervisorsEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var command = new AssignLeaveSupervisorsCommand(context.Message.Supervisors, context.Message.LeaveId);
        await sender.Send(command);
    }
}
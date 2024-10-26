using BuildingBlocks.Messaging.Events;
using Mapster;
using MassTransit;
using Microsoft.Extensions.Logging;

namespace Personnel.Api.Events.EventHandlers;

public class AsignLeaveSupervisorsHandler(IPublishEndpoint publishEndpoint, ILogger<AsignLeaveSupervisorsHandler> logger)
    : INotificationHandler<LeaveSupervisorsEvent>
{
    public async Task Handle(LeaveSupervisorsEvent domainEvent, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling LeaveSubmittedEvent: EventId");

        var leaveSubmittedIntegrationEvent = domainEvent.Adapt<SubmitLeaveEvent>();

        logger.LogInformation("Publishing SubmitLeaveEvent: {EventId}", leaveSubmittedIntegrationEvent.Id);

        try
        {
            await publishEndpoint.Publish(leaveSubmittedIntegrationEvent, cancellationToken);
            logger.LogInformation("Successfully published SubmitLeaveEvent: {EventId}",
                leaveSubmittedIntegrationEvent.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error publishing SubmitLeaveEvent: {EventId}", leaveSubmittedIntegrationEvent.Id);
        }
    }
}
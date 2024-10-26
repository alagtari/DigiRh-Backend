using BuildingBlocks.Messaging.Events;
using Mapster;
using Microsoft.Extensions.Logging;

namespace Conge.Application.Leaves.EventHandlers.Domain;

public class LeaveSubmittedEventHandler : INotificationHandler<LeaveSubmittedEvent>
{
    private readonly IPublishEndpoint _publishEndpoint;
    private readonly ILogger<LeaveSubmittedEventHandler> _logger;

    public LeaveSubmittedEventHandler(IPublishEndpoint publishEndpoint, ILogger<LeaveSubmittedEventHandler> logger)
    {
        _publishEndpoint = publishEndpoint;
        _logger = logger;
    }

    public async Task Handle(LeaveSubmittedEvent domainEvent, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Handling LeaveSubmittedEvent: {EventId}", domainEvent.LeaveId);

        var leaveSubmittedIntegrationEvent = domainEvent.Adapt<SubmitLeaveEvent>();

        _logger.LogInformation("Publishing SubmitLeaveEvent: {EventId}", leaveSubmittedIntegrationEvent.Id);

        try
        {
            await _publishEndpoint.Publish(leaveSubmittedIntegrationEvent, cancellationToken);
            _logger.LogInformation("Successfully published SubmitLeaveEvent: {EventId}",
                leaveSubmittedIntegrationEvent.Id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error publishing SubmitLeaveEvent: {EventId}", leaveSubmittedIntegrationEvent.Id);
        }
    }
}
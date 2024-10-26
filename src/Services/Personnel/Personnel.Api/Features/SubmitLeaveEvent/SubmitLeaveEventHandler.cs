using BuildingBlocks.Messaging.Events;
using MassTransit;

public class SubmitLeaveEventHandler(ISender sender, ILogger<SubmitLeaveEventHandler> logger)
    : IConsumer<SubmitLeaveEvent>
{
    public async Task Consume(ConsumeContext<SubmitLeaveEvent> context)
    {
        logger.LogInformation("Integration Event handled: {IntegrationEvent}", context.Message.GetType().Name);
        var command = new SubmitLeaveCommand(context.Message.UserId, context.Message.LeaveId);
        await sender.Send(command);
    }
}
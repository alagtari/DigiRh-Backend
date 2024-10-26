namespace Conge.Application.Leaves.Commands.CreateLeave;

public class CreateLeaveHandler(IApplicationDbContext dbContext)
    : ICommandHandler<CreateLeaveCommand, CreateLeaveResult>
{
    public async Task<CreateLeaveResult> Handle(CreateLeaveCommand command, CancellationToken cancellationToken)
    {
        var order = command.Leave.ToLeave();
        dbContext.Leaves.Add(order);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateLeaveResult(order.ToLeaveDto());
    }
}
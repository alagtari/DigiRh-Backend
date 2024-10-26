using Conge.Application.Leaves.Commands.UpdateLeave;

namespace Conge.API.Endpoints;

public record UpdateLeaveRequest(LeaveDto Leave);

public record UpdateLeaveResponse(Guid Id);

public class UpdateLeave : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/", async (UpdateLeaveRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateLeaveCommand>();

                var result = await sender.Send(command);

                var response = result.Adapt<UpdateLeaveResponse>();

                return Results.Created($"/leaves/{response.Id}", response);
            })
            .WithName("UpdateLeave")
            .Produces<UpdateLeaveResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update Leave")
            .WithDescription("Update Leave");
    }
}
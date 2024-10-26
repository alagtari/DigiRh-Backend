using Conge.Application.Leaves.Commands.SubmitLeave;

namespace Conge.API.Endpoints;

public record SubmitLeaveRequest(Guid LeaveId);

public record SubmitLeaveResponse(LeaveDto Payload);

public class SubmitLeave : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/submit", async (SubmitLeaveRequest request, ISender sender) =>
            {
                var command = request.Adapt<SubmitLeaveCommand>();
                var result = await sender.Send(command);

                var response = result.Adapt<SubmitLeaveResponse>();

                return Results.Created($"/leaves/{response.Payload.Id}", response);
            })
            .WithName("SubmitLeave")
            .Produces<SubmitLeaveResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Submit Leave")
            .WithDescription("Submit Leave");
    }
}
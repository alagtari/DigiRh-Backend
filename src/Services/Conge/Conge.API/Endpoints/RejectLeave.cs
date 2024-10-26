using Conge.Application.Leaves.Commands.RejectLeave;
using Conge.Domain.ValueObjects;

namespace Conge.API.Endpoints;

public record RejectLeaveRequest(bool Success);

public record RejectLeaveResponse(Guid Id);

public class RejectLeave : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/reject/{leaveId}", async (string leaveId, HttpContext httpContext, ISender sender) =>
            {
                var userId = httpContext.Request.Headers["UserId"].FirstOrDefault();

                var command = new RejectLeaveCommand(
                    Guid.Parse(leaveId),
                    Guid.Parse(userId)
                );

                var result = await sender.Send(command);

                var response = result.Adapt<RejectLeaveResponse>();

                return Results.Created($"/leaves/{response.Id}", response);
            })
            .WithName("RejectLeave")
            .Produces<RejectLeaveResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Accept Leave")
            .WithDescription("Accept Leave");
    }
}
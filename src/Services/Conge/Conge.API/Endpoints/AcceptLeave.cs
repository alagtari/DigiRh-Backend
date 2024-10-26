using Conge.Application.Leaves.Commands.AcceptLeave;
using Conge.Domain.ValueObjects;

namespace Conge.API.Endpoints;

public record AcceptLeaveRequest(bool Success);

public record AcceptLeaveResponse(Guid Id);

public class AcceptLeave : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPut("/accept/{leaveId}", async (string leaveId, HttpContext httpContext, ISender sender) =>
            {
                var userId = httpContext.Request.Headers["UserId"].FirstOrDefault();

                var command = new AcceptLeaveCommand(
                    Guid.Parse(leaveId),
                    Guid.Parse(userId)
                );

                var result = await sender.Send(command);

                var response = result.Adapt<AcceptLeaveResponse>();

                return Results.Created($"/leaves/{response.Id}", response);
            })
            .WithName("AcceptLeave")
            .Produces<AcceptLeaveResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Accept Leave")
            .WithDescription("Accept Leave");
    }
}
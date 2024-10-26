using Conge.Application.Extensions;

namespace Conge.API.Endpoints;

public record CreateLeaveRequest(NewLeaveDto Leave);

public record CreateLeaveResponse(LeaveDto Payload);

public class CreateLeave : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (HttpContext httpContext, CreateLeaveRequest request, ISender sender) =>
            {
                var userId = httpContext.Request.Headers["UserId"].FirstOrDefault();

                var result =
                    await sender.Send(
                        new CreateLeaveCommand(Leave: request.Leave.ToCreateLeaveDto(Guid.Parse(userId!))));

                var response = result.Adapt<CreateLeaveResponse>();

                return Results.Created($"/leaves/{response.Payload.Id}", response);
            })
            .WithName("CreateLeave")
            .Produces<CreateLeaveResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Leave")
            .WithDescription("Create Leave");
    }
}
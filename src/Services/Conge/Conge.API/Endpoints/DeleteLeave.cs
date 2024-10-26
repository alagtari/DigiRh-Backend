using Conge.Application.Leaves.Commands.DeleteLeave;

namespace Conge.API.Endpoints;

public record DeleteLeaveResponse(Boolean Success);

public class DeleteLeave : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/{id}", async (Guid Id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteLeaveCommand(Id));

                var response = result.Adapt<DeleteLeaveResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteLeave")
            .Produces<DeleteLeaveResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Delete Leave")
            .WithDescription("Delete Leave");
    }
}
using BuildingBlocks.Pagination;
using Conge.Application.Leaves.Queries.GetLeaveSupervisors;

public record GetLeaveSupervisorsResponse(IEnumerable<SupervisorDesisionDto> Payload);

public class GetLeaveSupervisors : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/supervisors/{leaveId}", async (string leaveId, HttpContext httpContext, ISender sender) =>
            {
                var userId = httpContext.Request.Headers["UserId"].FirstOrDefault();

                if (string.IsNullOrEmpty(userId))
                {
                    return Results.BadRequest("UserId is missing from the headers.");
                }

                var result = await sender.Send(new GetLeaveSupervisorsQuery(Guid.Parse(leaveId)));

                var response = result.Adapt<GetLeaveSupervisorsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetLeaveSupervisors")
            .Produces<GetLeaveSupervisorsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Leaves")
            .WithDescription("Get Leaves");
    }
}
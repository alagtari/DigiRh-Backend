using BuildingBlocks.Pagination;
using Conge.Application.Leaves.Queries.GetLeaves;

public record GetLeavesResponse(IEnumerable<LeaveDto> Payload);

public class GetLeaves : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/", async (HttpContext httpContext, ISender sender) =>
            {
                var userId = httpContext.Request.Headers["UserId"].FirstOrDefault();

                if (string.IsNullOrEmpty(userId))
                {
                    return Results.BadRequest("UserId is missing from the headers.");
                }

                var result = await sender.Send(new GetLeavesQuery(Guid.Parse(userId)));
                
                var response = result.Adapt<GetLeavesResponse>();

                return Results.Ok(response);
            })
            .WithName("GetLeaves")
            .Produces<GetLeavesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Leaves")
            .WithDescription("Get Leaves");
    }
}
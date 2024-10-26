using BuildingBlocks.Pagination;
using Conge.Application.Leaves.Queries.GetLeavesBySupervisorId;

public record GetLeavesBySupervisorIdResponse(dynamic Payload);

public class GetLeavesBySupervisorId : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/bysupervisor", async (HttpContext httpContext, ISender sender) =>
            {
                var userId = httpContext.Request.Headers["UserId"].FirstOrDefault();

                if (string.IsNullOrEmpty(userId))
                {
                    return Results.BadRequest("UserId is missing from the headers.");
                }

                var result = await sender.Send(new GetLeavesBySupervisorIdQuery(Guid.Parse(userId)));

                var response = result.Adapt<GetLeavesBySupervisorIdResponse>();

                return Results.Ok(response);
            })
            .WithName("GetLeavesBySupervisorId")
            .Produces<GetLeavesBySupervisorIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Leaves")
            .WithDescription("Get Leaves");
    }
}
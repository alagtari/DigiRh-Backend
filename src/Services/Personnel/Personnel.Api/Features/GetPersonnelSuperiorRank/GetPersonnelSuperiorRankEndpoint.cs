public record GetPersonnelSuperiorRankResponse(List<Person> Payload);

public class GetPersonnelSuperiorRankEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/credits", async (ISender sender) =>
            {
                var result = await sender.Send(new GetPersonnelSuperiorRankQuery());
                var response = result.Adapt<GetPersonnelSuperiorRankResponse>();
                return Results.Ok(response);
            })
            .WithName("GetPersonnelSuperiorRank")
            .Produces<GetPersonnelSuperiorRankResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Credits")
            .WithDescription("Get Credits");
    }
}
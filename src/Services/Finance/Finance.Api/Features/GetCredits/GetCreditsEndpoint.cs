public record GetCreditsResponse(List<Credit> Payload);

public class GetCreditsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/credits", async (ISender sender) =>
            {
                var result = await sender.Send(new GetCreditsQuery());
                var response = result.Adapt<GetCreditsResponse>();
                return Results.Ok(response);
            })
            .WithName("GetCredits")
            .Produces<GetCreditsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Credits")
            .WithDescription("Get Credits");
    }
}
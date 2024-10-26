public record GetAvancesResponse(List<Avance> Payload);

public class GetAvancesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/avances", async (ISender sender) =>
            {
                var result = await sender.Send(new GetAvancesQuery());
                var response = result.Adapt<GetAvancesResponse>();
                return Results.Ok(response);
            })
            .WithName("GetAvances")
            .Produces<GetAvancesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Avances")
            .WithDescription("Get Avances");
    }
}
public record GetChequesResponse(List<Cheque> Payload);

public class GetChequesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/cheques", async (ISender sender) =>
            {
                var result = await sender.Send(new GetChequesQuery());
                var response = result.Adapt<GetChequesResponse>();
                return Results.Ok(response);
            })
            .WithName("GetCheques")
            .Produces<GetChequesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Cheques")
            .WithDescription("Get Cheques");
    }
}
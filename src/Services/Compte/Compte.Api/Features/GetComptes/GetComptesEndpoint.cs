public record GetComptesRequest(Guid UserId);

public record GetComptesResponse(List<Account> Payload);

public class GetComptesEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/comptes", async ([AsParameters] GetComptesRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetComptesQuery(request.UserId));
                var response = result.Adapt<GetComptesResponse>();
                return Results.Ok(response);
            })
            .WithName("GetComptes")
            .Produces<GetComptesResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Comptes")
            .WithDescription("Get Comptes");
    }
}
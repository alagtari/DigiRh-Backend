public record GetJornalsRequest(Guid CompteId);

public record GetJornalsResponse(List<Account> Payload);

public class GetJornalsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/jornal", async ([AsParameters] GetJornalsRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetJornalsQuery(request.CompteId));
                var response = result.Adapt<GetJornalsResponse>();
                return Results.Ok(response);
            })
            .WithName("GetJornals")
            .Produces<GetJornalsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Jornals")
            .WithDescription("Get Jornals");
    }
}
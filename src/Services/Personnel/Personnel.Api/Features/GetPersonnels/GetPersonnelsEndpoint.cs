public record GetPersonnelsResponse(List<Person> Payload);

public class GetPersonnelsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/credits", async (ISender sender) =>
            {
                var result = await sender.Send(new GetPersonnelsQuery());
                var response = result.Adapt<GetPersonnelsResponse>();
                return Results.Ok(response);
            })
            .WithName("GetPersonnels")
            .Produces<GetPersonnelsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Credits")
            .WithDescription("Get Credits");
    }
}
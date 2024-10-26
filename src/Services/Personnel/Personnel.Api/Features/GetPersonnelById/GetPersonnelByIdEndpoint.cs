public record GetPersonnelByIdResponse(List<Person> Payload);

public class GetPersonnelByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/credits", async (ISender sender) =>
            {
                var result = await sender.Send(new GetPersonnelByIdQuery());
                var response = result.Adapt<GetPersonnelByIdResponse>();
                return Results.Ok(response);
            })
            .WithName("GetPersonnelById")
            .Produces<GetPersonnelByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Credits")
            .WithDescription("Get Credits");
    }
}
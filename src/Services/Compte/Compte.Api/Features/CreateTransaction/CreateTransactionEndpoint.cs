public record CreateTransactionRequest(CreateTransactionDto Transaction);
public record CreateTransactionResponse(List<Account> Payload);

public class CreateTransactionEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/transaction", async (CreateTransactionRequest request, ISender sender) =>
            {
                var command = request.Adapt<CreateTransactionCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<CreateTransactionResponse>();
                return Results.Ok(response);
            })
            .WithName("CreateTransaction")
            .Produces<CreateTransactionResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .WithSummary("Get Comptes")
            .WithDescription("Get Comptes");
    }
}
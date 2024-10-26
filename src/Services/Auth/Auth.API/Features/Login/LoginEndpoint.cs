using Auth.API.Dtos;


namespace Auth.API.Features.Login;

public record LoginRequest(LoginDto Login);

public record LoginResponse(LoginResponseDto Payload);

public class Login : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/login", async (LoginRequest request, ISender sender) =>
            {
                var command = request.Adapt<LoginQuery>();
                var result = await sender.Send(command);
                var response = result.Adapt<LoginResponse>();
                return Results.Created($"/login", response);
            })
            .WithName("Login")
            .Produces<LoginResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create Leave")
            .WithDescription("Create Leave");
    }
}
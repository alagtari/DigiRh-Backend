

namespace Auth.API.Features.Login;

public record LoginQuery(LoginDto Login) : IQuery<LoginResult>;

public record LoginResult(LoginResponseDto Payload);

public class LoginHandler(ApplicationDbContext dbContext, TokenGenerator tokenGenerator)
    : IQueryHandler<LoginQuery, LoginResult>
{
    public async Task<LoginResult> Handle(LoginQuery query, CancellationToken cancellationToken)
    {
        var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Username == query.Login.Username);
        if (user == null)
        {
            throw new UserNotFoundException();
        }

        if (!BCrypt.Net.BCrypt.Verify(query.Login.Password, user.PasswordHash))
        {
            throw new UserNotFoundException();
        }

        var token = tokenGenerator.GenerateJwtToken(user);

        return new LoginResult(new LoginResponseDto
        {
            Role = user.Role,
            token = token,
        });
    }
}
public record GetCreditsQuery() : IQuery<GetCreditsResult>;

public record GetCreditsResult(List<Credit> Payload);

public class GetCreditsHandler(ApplicationDbContext dbContext) : IQueryHandler<GetCreditsQuery, GetCreditsResult>
{
    public async Task<GetCreditsResult> Handle(GetCreditsQuery query, CancellationToken cancellationToken)
    {
        var credits = await dbContext.Credits.ToListAsync(cancellationToken);
        return new GetCreditsResult(credits);
    }
}
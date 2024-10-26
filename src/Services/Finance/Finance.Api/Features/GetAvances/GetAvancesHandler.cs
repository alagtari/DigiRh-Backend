public record GetAvancesQuery() : IQuery<GetAvancesResult>;

public record GetAvancesResult(List<Avance> Payload);

public class GetAvancesHandler(ApplicationDbContext dbContext) : IQueryHandler<GetAvancesQuery, GetAvancesResult>
{
    public async Task<GetAvancesResult> Handle(GetAvancesQuery query, CancellationToken cancellationToken)
    {
        var credits = await dbContext.Avances.ToListAsync(cancellationToken);
        return new GetAvancesResult(credits);
    }
}
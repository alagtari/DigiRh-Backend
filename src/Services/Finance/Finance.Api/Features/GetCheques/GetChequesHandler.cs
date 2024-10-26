public record GetChequesQuery() : IQuery<GetChequesResult>;

public record GetChequesResult(List<Cheque> Payload);

public class GetChequesHandler(ApplicationDbContext dbContext) : IQueryHandler<GetChequesQuery, GetChequesResult>
{
    public async Task<GetChequesResult> Handle(GetChequesQuery query, CancellationToken cancellationToken)
    {
        var credits = await dbContext.Cheques.ToListAsync(cancellationToken);
        return new GetChequesResult(credits);
    }
}
public record GetComptesQuery(Guid UserId) : IQuery<GetComptesResult>;

public record GetComptesResult(List<Account> Payload);

public class GetComptesHandler(ApplicationDbContext dbContext) : IQueryHandler<GetComptesQuery, GetComptesResult>
{
    public async Task<GetComptesResult> Handle(GetComptesQuery query, CancellationToken cancellationToken)
    {
        var comptes = await dbContext.Comptes.Where(c => c.UserId == query.UserId).ToListAsync(cancellationToken);
        return new GetComptesResult(comptes);
    }
}
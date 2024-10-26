public record GetJornalsQuery(Guid CompteId) : IQuery<GetJornalsResult>;

public record GetJornalsResult(List<Jornal> Payload);

public class GetJornalsHandler(ApplicationDbContext dbContext) : IQueryHandler<GetJornalsQuery, GetJornalsResult>
{
    public async Task<GetJornalsResult> Handle(GetJornalsQuery query, CancellationToken cancellationToken)
    {
        var comptes = await dbContext.Jornals
            .Where(c => c.CompteSource == query.CompteId || c.CompteDistination == query.CompteId)
            .ToListAsync(cancellationToken);
        return new GetJornalsResult(comptes);
    }
}
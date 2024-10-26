public record GetPersonnelSuperiorRankQuery() : IQuery<GetPersonnelSuperiorRankResult>;

public record GetPersonnelSuperiorRankResult(List<Person> Payload);

public class GetPersonnelSuperiorRankHandler(ApplicationDbContext dbContext)
    : IQueryHandler<GetPersonnelSuperiorRankQuery, GetPersonnelSuperiorRankResult>
{
    public async Task<GetPersonnelSuperiorRankResult> Handle(GetPersonnelSuperiorRankQuery query,
        CancellationToken cancellationToken)
    {
        var persons = await dbContext.Persons.ToListAsync(cancellationToken);
        return new GetPersonnelSuperiorRankResult(persons);
    }
}
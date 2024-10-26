public record GetPersonnelsQuery() : IQuery<GetPersonnelsResult>;

public record GetPersonnelsResult(List<Person> Payload);

public class GetPersonnelsHandler(ApplicationDbContext dbContext)
    : IQueryHandler<GetPersonnelsQuery, GetPersonnelsResult>
{
    public async Task<GetPersonnelsResult> Handle(GetPersonnelsQuery query, CancellationToken cancellationToken)
    {
        var persons = await dbContext.Persons.ToListAsync(cancellationToken);
        return new GetPersonnelsResult(persons);
    }
}
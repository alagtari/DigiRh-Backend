public record GetPersonnelByIdQuery() : IQuery<GetPersonnelByIdResult>;

public record GetPersonnelByIdResult(List<Person> Payload);

public class GetPersonnelByIdHandler(ApplicationDbContext dbContext)
    : IQueryHandler<GetPersonnelByIdQuery, GetPersonnelByIdResult>
{
    public async Task<GetPersonnelByIdResult> Handle(GetPersonnelByIdQuery query, CancellationToken cancellationToken)
    {
        var persons = await dbContext.Persons.ToListAsync(cancellationToken);
        return new GetPersonnelByIdResult(persons);
    }
}
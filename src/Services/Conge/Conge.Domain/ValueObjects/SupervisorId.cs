
namespace Conge.Domain.ValueObjects;

public class SupervisorId
{
    public Guid Value { get; }
    private SupervisorId(Guid value) => Value = value;

    public static SupervisorId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("CongeId cannot be empty.");
        }
        return new SupervisorId(value);
    }
}

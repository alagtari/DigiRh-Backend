
namespace Conge.Domain.ValueObjects;

public class LeaveId
{
    public Guid Value { get; }
    private LeaveId(Guid value) => Value = value;

    public static LeaveId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("CongeId cannot be empty.");
        }
        return new LeaveId(value);
    }
}

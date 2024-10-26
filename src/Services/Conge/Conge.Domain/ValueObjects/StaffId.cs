using Conge.Domain.Exceptions;

namespace Conge.Domain.ValueObjects;

public class StaffId
{
    public Guid Value { get; }
    private StaffId(Guid value) => Value = value;

    public static StaffId Of(Guid value)
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value == Guid.Empty)
        {
            throw new DomainException("StaffId cannot be empty.");
        }

        return new StaffId(value);
    }
}
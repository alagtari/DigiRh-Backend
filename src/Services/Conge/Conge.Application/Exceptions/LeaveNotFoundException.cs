namespace Conge.Application.Exceptions;

public class LeaveNotFoundException : NotFoundException
{
    public LeaveNotFoundException(Guid id) : base("Order", id)
    {
    }
}
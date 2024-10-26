namespace Finance.API.Exceptions;

public class UserNotFoundException : NotFoundException
{
    public UserNotFoundException() : base("Incorrect username or password")
    {
    }
}
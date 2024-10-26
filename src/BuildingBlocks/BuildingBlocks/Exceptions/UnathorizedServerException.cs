namespace BuildingBlocks.Exceptions;

public class UnathorizedServerException : Exception
{
    public UnathorizedServerException() : base("Not authorized!")
    {
    }
}
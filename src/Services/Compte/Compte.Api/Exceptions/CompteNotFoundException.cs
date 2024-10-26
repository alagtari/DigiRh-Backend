namespace Compte.API.Exceptions;

public class CompteNotFoundException : NotFoundException
{
    public CompteNotFoundException() : base("Compte introuvable!")
    {
    }
}
namespace Compte.API.Exceptions;

public class SoldeInsuffisantException : Exception
{
    public SoldeInsuffisantException() : base("Solde Insuffisant!")
    {
    }
}
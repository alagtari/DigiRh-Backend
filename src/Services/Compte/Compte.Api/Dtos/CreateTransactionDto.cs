namespace Compte.Api.Dtos;

public class CreateTransactionDto
{
    public Guid CompteSource { get; set; }
    public Guid CompteDestination { get; set; }
    public double Montant { get; set; }
}
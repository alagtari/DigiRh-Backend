namespace Compte.API.Models;

public class Jornal
{
    public Guid Id { get; set; }
    public string OperationName { get; set; }
    public DateTime DateOperation { get; set; }
    public DateTime DateValeur { get; set; }
    public double Montant { get; set; }
    public Guid CompteSource { get; set; }
    public Guid CompteDistination { get; set; }
}
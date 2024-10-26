namespace Finance.API.Models;

public class Cheque
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Numero { get; set; }
    public double Montant { get; set; }
    public DateTime DateOperation { get; set; }
    public DateTime DateValeur { get; set; }
}
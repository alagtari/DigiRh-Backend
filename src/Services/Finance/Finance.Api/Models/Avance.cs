namespace Finance.API.Models;

public class Avance
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public double MontantEcheance { get; set; }
    public DateTime DateEcheance { get; set; }
}
namespace Finance.API.Models;

public class Credit
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public double Montant { get; set; }
    public double EnCours { get; set; }
    public double Mensualite { get; set; }
    public DateTime DateDebut { get; set; }
    public DateTime DateFin { get; set; }
}
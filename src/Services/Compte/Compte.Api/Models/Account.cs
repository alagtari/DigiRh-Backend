using Compte.Domain.Enums;

namespace Compte.API.Models;

public class Account
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string UserName { get; set; }

    public string NomCompte { get; set; }
    public string NumeroCompte { get; set; }
    public double Solde { get; set; }
    public double SoldeDisponible { get; set; }
    public AccountType AccountType { get; set; }
}
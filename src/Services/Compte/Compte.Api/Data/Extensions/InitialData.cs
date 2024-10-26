namespace Compte.API.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Account> Accounts =>
        new List<Account>
        {
            new Account
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "Ala Gtari",
                NomCompte = "COMPTE EPARGNE STB",
                NumeroCompte = "1001XXXX030788",
                Solde = 1000.0,
                SoldeDisponible = 1000.0,
                AccountType = AccountType.Epargne
            },
            new Account
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                UserName = "Ala Gtari",
                NomCompte = "COMPTE CHEQUE PERSONNEL STB",
                NumeroCompte = "2201XXXX030321",
                Solde = 100.0,
                SoldeDisponible = -1000.0,
                AccountType = AccountType.Cheque
            },
        };

    public static IEnumerable<Jornal> Jornals =>
        new List<Jornal>
        {
            new Jornal
            {
                Id = Guid.NewGuid(),
            },
        };
}
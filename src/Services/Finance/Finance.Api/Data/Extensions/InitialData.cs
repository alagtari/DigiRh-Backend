namespace Finance.API.Data.Extensions;

internal class InitialData
{
    public static IEnumerable<Avance> Avances =>
        new List<Avance>
        {
            new Avance
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                DateEcheance = new DateTime(),
                MontantEcheance = 1000.0
            },
        };

    public static IEnumerable<Credit> Credits =>
        new List<Credit>
        {
            new Credit
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Montant = 1000.0,
                EnCours = 100.0,
                Mensualite = 100.0,
                DateDebut = new DateTime(),
                DateFin = new DateTime(),
            },
        };

    public static IEnumerable<Cheque> Cheques =>
        new List<Cheque>
        {
            new Cheque
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Montant = 1000.0,
                Numero = "f2123",
                DateOperation = new DateTime(),
                DateValeur = new DateTime(),
            },
        };
}
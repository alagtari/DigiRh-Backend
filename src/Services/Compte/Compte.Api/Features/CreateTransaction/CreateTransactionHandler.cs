using Compte.API.Exceptions;

public record CreateTransactionCommand(CreateTransactionDto Request) : ICommand<CreateTransactionResult>;

public record CreateTransactionResult(Jornal Payload);

public class CreateTransactionHandler(ApplicationDbContext dbContext)
    : ICommandHandler<CreateTransactionCommand, CreateTransactionResult>
{
    public async Task<CreateTransactionResult> Handle(CreateTransactionCommand command,
        CancellationToken cancellationToken)
    {
        var compteSource = await dbContext.Comptes.Where(c => c.Id == command.Request.CompteSource)
            .SingleOrDefaultAsync(cancellationToken);
        if (compteSource != null)
        {
            if (compteSource.SoldeDisponible >= command.Request.Montant)
            {
                var compteDestination = await dbContext.Comptes.Where(c => c.Id == command.Request.CompteDestination)
                    .SingleOrDefaultAsync(cancellationToken);
                if (compteDestination != null)
                {
                    compteSource.SoldeDisponible -= command.Request.Montant;
                    compteSource.Solde -= command.Request.Montant;
                    compteSource.Solde += command.Request.Montant;
                    var jornal = new Jornal
                    {
                        Id = Guid.NewGuid(),
                        OperationName = "Transaction compte",
                        DateOperation = DateTime.Now,
                        DateValeur = DateTime.Now.AddDays(1),
                        Montant = command.Request.Montant,
                        CompteSource = command.Request.CompteSource,
                        CompteDistination = command.Request.CompteDestination,
                    };

                    dbContext.Jornals.Add(jornal);
                    await dbContext.SaveChangesAsync(cancellationToken);
                    return new CreateTransactionResult(jornal);
                }
                else
                {
                    throw new CompteNotFoundException();
                }
            }
            else
            {
                throw new SoldeInsuffisantException();
            }
        }
        else
        {
            throw new CompteNotFoundException();
        }
    }
}
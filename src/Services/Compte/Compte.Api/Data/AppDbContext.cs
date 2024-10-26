namespace Compte.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Account> Comptes => Set<Account>();
    public DbSet<Jornal> Jornals => Set<Jornal>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new CompteConfiguration());
    }
}
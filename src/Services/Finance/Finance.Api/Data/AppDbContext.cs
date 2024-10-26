namespace Finance.API.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Avance> Avances => Set<Avance>();
    public DbSet<Credit> Credits => Set<Credit>();
    public DbSet<Cheque> Cheques => Set<Cheque>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfiguration(new AvanceConfiguration());
    }
}
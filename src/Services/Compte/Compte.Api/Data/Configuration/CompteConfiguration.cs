namespace Compte.API.Data.Configuration;

public class CompteConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder
            .HasIndex(u => u.Id)
            .IsUnique();
    }
}
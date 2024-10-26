namespace Finance.API.Data.Configuration;

public class CreditConfiguration : IEntityTypeConfiguration<Credit>
{
    public void Configure(EntityTypeBuilder<Credit> builder)
    {
        builder
            .HasIndex(u => u.Id)
            .IsUnique();
    }
}
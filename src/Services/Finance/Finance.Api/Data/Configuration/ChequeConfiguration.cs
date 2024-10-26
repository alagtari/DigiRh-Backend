namespace Finance.API.Data.Configuration;

public class ChequeConfiguration : IEntityTypeConfiguration<Cheque>
{
    public void Configure(EntityTypeBuilder<Cheque> builder)
    {
        builder
            .HasIndex(u => u.Id)
            .IsUnique();
    }
}
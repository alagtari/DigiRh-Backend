namespace Finance.API.Data.Configuration;

public class AvanceConfiguration : IEntityTypeConfiguration<Avance>
{
    public void Configure(EntityTypeBuilder<Avance> builder)
    {
        builder
            .HasIndex(u => u.Id)
            .IsUnique();
    }
}
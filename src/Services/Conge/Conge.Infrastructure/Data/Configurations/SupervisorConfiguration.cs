namespace Conge.Infrastructure.Data.Configurations;

public class SupervisorConfiguration : IEntityTypeConfiguration<Supervisor>
{
    public void Configure(EntityTypeBuilder<Supervisor> builder)
    {
        builder.HasKey(s => s.Id);

        builder.Property(s => s.Id)
            .HasConversion(
                supervisorId => supervisorId.Value,
                dbId => SupervisorId.Of(dbId));

        builder.Property(l => l.Prenom)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(l => l.Nom)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(l => l.Image)
            .HasMaxLength(255)
            .IsRequired();

        builder.Property(l => l.Accord)
            .HasDefaultValue(LeaveAgreement.Waiting)
            .HasConversion(
                status => status.ToString(),
                dbStatus => (LeaveAgreement)Enum.Parse(typeof(LeaveAgreement), dbStatus));
    }
}
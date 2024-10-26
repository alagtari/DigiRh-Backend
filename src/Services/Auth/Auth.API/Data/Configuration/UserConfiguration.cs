namespace Auth.API.Data.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder
            .HasIndex(u => u.Username)
            .IsUnique();

        builder
            .HasIndex(u => u.Email)
            .IsUnique();

        builder
            .Property(u => u.Username)
            .HasMaxLength(50)
            .IsRequired();

        builder
            .Property(u => u.Email)
            .HasMaxLength(100)
            .IsRequired();
    }
}
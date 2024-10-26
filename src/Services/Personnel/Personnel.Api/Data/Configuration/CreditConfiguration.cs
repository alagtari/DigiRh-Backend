using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Personnel.API.Models;

namespace Personnel.API.Data.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasIndex(u => u.Id).IsUnique();


            builder
                .HasOne(p => p.Supervisor)
                .WithMany()
                .HasForeignKey(p => p.SupervisorId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.Property(p => p.Matricule)
                .IsRequired()
                .HasMaxLength(10);


            builder.Property(p => p.Prenom)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.Nom)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.NumeroTelephone)
                .IsRequired()
                .HasMaxLength(15);


            builder.Property(p => p.Image)
                .HasMaxLength(255);
        }
    }
}
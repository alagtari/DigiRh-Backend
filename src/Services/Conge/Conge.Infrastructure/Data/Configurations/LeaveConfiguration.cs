public class LeaveConfiguration : IEntityTypeConfiguration<Leave>
{
    public void Configure(EntityTypeBuilder<Leave> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Id)
            .HasConversion(
                leaveId => leaveId.Value,
                dbId => LeaveId.Of(dbId));

        builder.Property(l => l.StaffId)
            .HasConversion(
                staffId => staffId.Value,
                dbId => StaffId.Of(dbId));

        builder.Property(l => l.LeaveType)
            .HasConversion(
                leaveType => leaveType.ToString(),
                dbLeaveType => (LeaveType)Enum.Parse(typeof(LeaveType), dbLeaveType))
            .IsRequired();

        builder.Property(l => l.StartDate)
            .IsRequired();

        builder.Property(l => l.EndDate)
            .IsRequired();

        builder.Property(l => l.Phone)
            .HasMaxLength(20)
            .IsRequired();

        builder.Property(l => l.Pay)
            .HasMaxLength(50)
            .IsRequired();

        builder.Property(l => l.Status)
            .HasDefaultValue(LeaveStatus.Draft)
            .HasConversion(
                status => status.ToString(),
                dbStatus => (LeaveStatus)Enum.Parse(typeof(LeaveStatus), dbStatus));

        builder.HasMany(l => l.Supervisors)
            .WithOne()
            .HasForeignKey(s => s.LeaveId);
    }
}
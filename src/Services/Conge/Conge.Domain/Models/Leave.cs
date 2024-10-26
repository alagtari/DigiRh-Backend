namespace Conge.Domain.Models
{
    public class Leave : Aggregate<LeaveId>
    {
        private readonly List<Supervisor> _supervisors = new();
        public IReadOnlyList<Supervisor> Supervisors => _supervisors.AsReadOnly();
        public LeaveId Id { get; private set; } = default!;
        public StaffId StaffId { get; private set; } = default!;
        public LeaveType LeaveType { get; private set; } = default!;
        public DateTime StartDate { get; private set; } = default!;
        public DateTime EndDate { get; private set; } = default!;
        public string Phone { get; private set; } = default!;
        public string Pay { get; private set; } = default!;
        public LeaveStatus Status { get; private set; } = LeaveStatus.Draft;

        // Factory method to create a new Leave
        public static Leave Create(StaffId staffId, LeaveType leaveType, DateTime startDate, DateTime endDate,
            string phone, string pay)
        {
            var leave = new Leave
            {
                Id = LeaveId.Of(Guid.NewGuid()),
                StaffId = staffId,
                LeaveType = leaveType,
                StartDate = startDate,
                EndDate = endDate,
                Phone = phone,
                Pay = pay,
                Status = LeaveStatus.Draft
            };

            // Trigger domain event when leave is created
            leave.AddDomainEvent(new LeaveCreatedEvent(leave));

            return leave;
        }

        // Method to update leave details
        public void Update(LeaveType? leaveType, DateTime? startDate, DateTime? endDate, string phone, string pay,
            LeaveStatus? status)
        {
            if (leaveType != null)
                LeaveType = leaveType.Value;

            if (startDate != null)
                StartDate = startDate.Value;

            if (endDate != null)
                EndDate = endDate.Value;

            if (!string.IsNullOrEmpty(phone))
                Phone = phone;

            if (!string.IsNullOrEmpty(pay))
                Pay = pay;

            if (status != null)
            {
                Status = status.Value;
                if (status == LeaveStatus.Pending)
                {
                    // Trigger domain event when leave is submitted
                    AddDomainEvent(new LeaveSubmittedEvent(this.StaffId.Value, this.Id.Value));
                }
            }
        }

        // Method to add a supervisor to the leave request
        public void AddSupervisor(Supervisor supervisor)
        {
            _supervisors.Add(supervisor);
        }

        
    }
}
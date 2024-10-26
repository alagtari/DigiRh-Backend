using Conge.Domain.Enums;

namespace Conge.Application.Dtos;

public class CreateLeaveDto
{
    public Guid StaffId { get; set; }
    public LeaveType LeaveType { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public string Phone { get; set; } = string.Empty;
    public string Pay { get; set; } = string.Empty;
}
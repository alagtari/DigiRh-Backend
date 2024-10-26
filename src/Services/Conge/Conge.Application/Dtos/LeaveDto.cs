using Conge.Domain.Enums;

namespace Conge.Application.Dtos;

public record LeaveDto(
    Guid Id,
    Guid StaffId,
    LeaveType LeaveType,
    DateTime StartDate,
    DateTime EndDate,
    string Phone,
    string Pay,
    LeaveStatus Status
);


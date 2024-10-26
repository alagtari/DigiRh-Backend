using Conge.Domain.Enums;

namespace Conge.Application.Dtos;

public record SupervisorLeaveDto(
    Guid Id,
    Guid StaffId,
    LeaveType LeaveType,
    DateTime StartDate,
    DateTime EndDate,
    string Phone,
    string Pay,
    LeaveStatus Status,
    Guid? DesisionId,
    LeaveAgreement? Desision
);
using Conge.Application.Dtos;
using Conge.Domain.Models;
using Conge.Domain.ValueObjects;

namespace Conge.Application.Extensions;

public static class LeaveExtensions
{
    public static IEnumerable<LeaveDto> ToLeaveDtoList(this IEnumerable<Leave> leaves)
    {
        return leaves.Select(leave => leave.ToLeaveDto());
    }

    public static LeaveDto ToLeaveDto(this Leave leave)
    {
        return new LeaveDto(
            Id: leave.Id.Value,
            StaffId: leave.StaffId.Value,
            StartDate: leave.StartDate,
            EndDate: leave.EndDate,
            LeaveType: leave.LeaveType,
            Status: leave.Status,
            Phone: leave.Phone,
            Pay: leave.Pay
        );
    }


    public static SupervisorLeaveDto ToSupervisorLeaveDto(this Leave leave, Supervisor? supervisor)
    {
        return new SupervisorLeaveDto(
            Id: leave.Id.Value,
            StaffId: leave.StaffId.Value,
            StartDate: leave.StartDate,
            EndDate: leave.EndDate,
            LeaveType: leave.LeaveType,
            Status: leave.Status,
            Phone: leave.Phone,
            Pay: leave.Pay,
            DesisionId: supervisor?.Id.Value,
            Desision: supervisor?.Accord
        );
    }


    public static Leave ToLeave(this CreateLeaveDto leaveCreateDto)
    {
        return Leave.Create(
            StaffId.Of(leaveCreateDto.StaffId),
            leaveCreateDto.LeaveType,
            leaveCreateDto.StartDate,
            leaveCreateDto.EndDate,
            leaveCreateDto.Phone,
            leaveCreateDto.Pay
        );
    }

    public static CreateLeaveDto ToCreateLeaveDto(this NewLeaveDto leaveCreateDto, Guid userId)
    {
        return new CreateLeaveDto
        {
            StaffId = userId,
            LeaveType = leaveCreateDto.LeaveType,
            StartDate = leaveCreateDto.StartDate,
            EndDate = leaveCreateDto.EndDate,
            Phone = leaveCreateDto.Phone,
            Pay = leaveCreateDto.Pay
        };
    }
}
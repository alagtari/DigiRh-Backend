using BuildingBlocks.Pagination;
using Personnel.Api.Dtos;

namespace Conge.Application.Leaves.Queries.GetLeaveSupervisors;

public record GetLeaveSupervisorsQuery(Guid LeaveId)
    : IQuery<GetLeaveSupervisorsResult>;

public record GetLeaveSupervisorsResult(IEnumerable<SupervisorDesisionDto> Payload);
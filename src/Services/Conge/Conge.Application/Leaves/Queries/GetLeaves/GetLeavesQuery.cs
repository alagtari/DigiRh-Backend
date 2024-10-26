using BuildingBlocks.Pagination;

namespace Conge.Application.Leaves.Queries.GetLeaves;

public record GetLeavesQuery(Guid UserId)
    : IQuery<GetLeavesResult>;

public record GetLeavesResult(IEnumerable<LeaveDto> Payload);
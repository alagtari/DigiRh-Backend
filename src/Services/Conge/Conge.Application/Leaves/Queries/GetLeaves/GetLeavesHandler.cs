using Conge.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Conge.Application.Leaves.Queries.GetLeaves;

using BuildingBlocks.Pagination;

public class GetLeavesHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetLeavesQuery, GetLeavesResult>
{
    public async Task<GetLeavesResult> Handle(GetLeavesQuery query, CancellationToken cancellationToken)
    {
        var leaves = await dbContext.Leaves
            .Where(l => l.StaffId == StaffId.Of(query.UserId))
            .ToListAsync(cancellationToken);

        return new GetLeavesResult(leaves.ToLeaveDtoList());
    }
}
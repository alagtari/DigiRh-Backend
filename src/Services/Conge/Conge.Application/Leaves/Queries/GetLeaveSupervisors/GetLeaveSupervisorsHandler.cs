using Conge.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Conge.Application.Leaves.Queries.GetLeaveSupervisors;

using BuildingBlocks.Pagination;

public class GetLeaveSupervisorsHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetLeaveSupervisorsQuery, GetLeaveSupervisorsResult>
{
    public async Task<GetLeaveSupervisorsResult> Handle(GetLeaveSupervisorsQuery query,
        CancellationToken cancellationToken)
    {
        var supervisors = await dbContext.Supervisors
            .Where(s => s.LeaveId == LeaveId.Of(query.LeaveId))
            .ToListAsync(cancellationToken);


        return new GetLeaveSupervisorsResult(supervisors.ToSupervisorDesisionDtoList());
    }
}
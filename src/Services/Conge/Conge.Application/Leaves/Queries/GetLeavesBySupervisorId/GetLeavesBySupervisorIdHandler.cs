using Conge.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BuildingBlocks.Pagination;

namespace Conge.Application.Leaves.Queries.GetLeavesBySupervisorId;

public class GetLeavesBySupervisorIdHandler(
    IApplicationDbContext dbContext,
    ILogger<GetLeavesBySupervisorIdHandler> logger)
    : IQueryHandler<GetLeavesBySupervisorIdQuery, GetLeavesBySupervisorIdResult>
{
    public async Task<GetLeavesBySupervisorIdResult> Handle(GetLeavesBySupervisorIdQuery query,
        CancellationToken cancellationToken)
    {
        try
        {
            var leavesWithSupervisors = await dbContext.Leaves
                .Where(l => l.Supervisors.Any(s => s.PersonnelId == query.UserId))
                .Select(l => new
                {
                    Leave = l.ToSupervisorLeaveDto(
                        l.Supervisors
                            .Where(s => s.PersonnelId == query.UserId)
                            .Single())
                })
                .ToListAsync(cancellationToken);


            var result = leavesWithSupervisors
                .Select(lws => lws.Leave
                )
                .ToList();

            return new GetLeavesBySupervisorIdResult(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while fetching leaves for supervisor with ID {UserId}",
                query.UserId);
            throw;
        }
    }
}
using BuildingBlocks.Pagination;
using Conge.Domain.Models;
using Personnel.Api.Dtos;

namespace Conge.Application.Leaves.Queries.GetLeavesBySupervisorId;

public record GetLeavesBySupervisorIdQuery(Guid UserId)
    : IQuery<GetLeavesBySupervisorIdResult>;

public record GetLeavesBySupervisorIdResult(dynamic Payload);
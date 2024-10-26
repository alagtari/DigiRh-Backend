using Conge.Application.Dtos;
using Conge.Domain.Models;
using Conge.Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace Conge.Application.Extensions;

public static class SupervisorExtensions
{
    public static IEnumerable<SupervisorDesisionDto> ToSupervisorDesisionDtoList(
        this IEnumerable<Supervisor> supervisors)
    {
        return supervisors.Select(supervisor => supervisor.ToSupervisorDesisionDto());
    }

    public static SupervisorDesisionDto ToSupervisorDesisionDto(this Supervisor supervisor)
    {
        return new SupervisorDesisionDto(
            supervisor.Id.Value,
            supervisor.Prenom,
            supervisor.Nom,
            supervisor.Image,
            supervisor.Accord,
            supervisor.CreatedAt,
            supervisor.LastModified
        );
    }
}
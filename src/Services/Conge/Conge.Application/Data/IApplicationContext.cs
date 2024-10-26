using Conge.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Conge.Application.Data;

public interface IApplicationDbContext
{
    DbSet<Leave> Leaves { get; }
    DbSet<Supervisor> Supervisors { get; }


    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
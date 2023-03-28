using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Microsoft.EntityFrameworkCore;

namespace Learn.Ddd.TaskTracker.Application.Interfaces.Persistence;

public interface IDataContext
{
	DbSet<Product> Products { get; set; }
	DbSet<Backlog> Backlogs { get; set; }
	DbSet<Sprint> Sprints { get; set; }
	DbSet<Issue> Issues { get; set; }
	DbSet<Team> Teams { get; set; }
	DbSet<TeamMember> TeamMembers { get; set; }
	DbSet<ProductOwner> ProductOwners { get; set; }

	Task<int> SaveChangesAsync(CancellationToken cancellationToken = new());
}
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;

namespace Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;

public interface ITeamRepository
{
	Task<Team?> GetTeamByIdAsync(Guid teamId, CancellationToken cancellationToken = default);
	Task<Team?> GetTeamByIdIncludeMembersAsync(Guid teamId, CancellationToken cancellationToken = default);
}
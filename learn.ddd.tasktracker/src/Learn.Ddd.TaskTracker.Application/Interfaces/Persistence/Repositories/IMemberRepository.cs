using Learn.Ddd.TaskTracker.Domain.Entities.Teams;

namespace Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;

public interface IMemberRepository
{
	Task<TeamMember?> GetTeamMemberByIdAsync(Guid memberId, CancellationToken cancellationToken = default);
	Task CreateTeamMemberAsync(TeamMember member, CancellationToken cancellationToken = default);
}
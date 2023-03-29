using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Learn.Ddd.TaskTracker.Infrastructure.Persistence;

namespace Learn.Ddd.TaskTracker.Infrastructure.Repositories;

public class MemberRepository : IMemberRepository
{
	private readonly DataContext _context;

	public MemberRepository(DataContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	/// <inheritdoc />
	public async Task<TeamMember?> GetTeamMemberByIdAsync(Guid memberId, CancellationToken cancellationToken = default)
	{
		return await _context.Set<TeamMember>()
			.FindAsync(memberId, cancellationToken);
	}

	/// <inheritdoc />
	public async Task CreateTeamMemberAsync(TeamMember member, CancellationToken cancellationToken = default)
	{
		await _context.Set<TeamMember>()
			.AddAsync(member ?? throw new ArgumentNullException(nameof(member)),
				cancellationToken);
	}
}
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Learn.Ddd.TaskTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Learn.Ddd.TaskTracker.Infrastructure.Repositories;

public class TeamRepository : ITeamRepository
{
	private readonly DataContext _context;

	public TeamRepository(DataContext context) 
		=> _context = context ?? throw new ArgumentNullException(nameof(context));

	/// <inheritdoc />
	public async Task<Team?> GetTeamByIdAsync(Guid teamId, CancellationToken cancellationToken = default)
		=> await _context.Set<Team>()
			.FindAsync(teamId, cancellationToken);

	/// <inheritdoc />
	public async Task<Team?> GetTeamByIdIncludeMembersAsync(Guid teamId, CancellationToken cancellationToken = default)
		=> await _context.Set<Team>()
			.Where(team => team.Id == teamId)
			.Include(team => team.Members)
			.FirstOrDefaultAsync(cancellationToken);
}
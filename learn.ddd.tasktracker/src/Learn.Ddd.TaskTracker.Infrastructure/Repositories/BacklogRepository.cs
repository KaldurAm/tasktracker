using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Learn.Ddd.TaskTracker.Infrastructure.Repositories;

public class BacklogRepository : IBacklogRepository
{
	private readonly DataContext _context;

	public BacklogRepository(DataContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	/// <inheritdoc />
	public async Task<Backlog?> GetBacklogByIdAsync(Guid backlogId, CancellationToken cancellationToken = default)
	{
		return await _context.Set<Backlog>()
			.FindAsync(backlogId, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<Backlog?> GetBacklogByIdWithIssuesAsync(Guid backlogId, CancellationToken cancellationToken = default)
	{
		return await _context.Set<Backlog>()
			.Where(x => x.Id == backlogId)
			.Include(i => i.Issues)
			.FirstOrDefaultAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task<IEnumerable<Issue>> GetBacklogIssuesAsync(Guid backlogId, CancellationToken cancellationToken = default)
	{
		return await _context.Set<Issue>()
			.Where(i => i.BacklogId == backlogId)
			.OrderBy(o => o.CreatedAt)
			.ToListAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task<Issue?> GetBacklogIssueByIdAsync(Guid issueId, CancellationToken cancellationToken = default)
	{
		return await _context.Set<Issue>()
			.FindAsync(issueId, cancellationToken);
	}
}
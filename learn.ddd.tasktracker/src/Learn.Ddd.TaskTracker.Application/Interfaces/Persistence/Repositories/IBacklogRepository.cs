using Learn.Ddd.TaskTracker.Domain.Entities.Products;

namespace Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;

public interface IBacklogRepository
{
	Task<Backlog?> GetBacklogByIdAsync(Guid backlogId, CancellationToken cancellationToken = default);
	Task<Backlog?> GetBacklogByIdWithIssuesAsync(Guid backlogId, CancellationToken cancellationToken = default);
	Task<IEnumerable<Issue>> GetBacklogIssuesAsync(Guid backlogId, CancellationToken cancellationToken = default);
	Task<Issue?> GetBacklogIssueByIdAsync(Guid issueId, CancellationToken cancellationToken = default);
}
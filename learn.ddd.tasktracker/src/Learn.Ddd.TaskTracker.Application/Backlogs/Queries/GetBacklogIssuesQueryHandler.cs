using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Backlogs.Queries;

public record GetBacklogIssuesQueryHandler : IRequestHandler<GetBacklogIssuesQuery, Result<IEnumerable<Issue>>>
{
	private readonly IBacklogRepository _backlogRepository;
	private readonly ILogger<GetBacklogIssuesQueryHandler> _logger;

	public GetBacklogIssuesQueryHandler(ILogger<GetBacklogIssuesQueryHandler> logger, IBacklogRepository backlogRepository)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_backlogRepository = backlogRepository ?? throw new ArgumentNullException(nameof(backlogRepository));
	}

	/// <inheritdoc />
	public async Task<Result<IEnumerable<Issue>>> Handle(GetBacklogIssuesQuery request, CancellationToken cancellationToken)
	{
		var backlog = await _backlogRepository.GetBacklogByIdWithIssuesAsync(request.BacklogId, cancellationToken);

		if (backlog is null)
		{
			_logger.LogWarning("Backlog not found by id {BacklogId}", request.BacklogId);

			return Result.Failure<IEnumerable<Issue>>(DomainError.Product.NotFoundBacklog);
		}

		return Result.Success(backlog.Issues);
	}
}
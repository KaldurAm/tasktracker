using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Issues.Queries;

public record GetIssueByIdQueryHandler : IRequestHandler<GetIssueByIdQuery, Result<Issue>>
{
	private readonly IBacklogRepository _backlogRepository;
	private readonly ILogger<GetIssueByIdQueryHandler> _logger;

	public GetIssueByIdQueryHandler(ILogger<GetIssueByIdQueryHandler> logger, IBacklogRepository backlogRepository)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_backlogRepository = backlogRepository ?? throw new ArgumentNullException(nameof(backlogRepository));
	}

	/// <inheritdoc />
	public async Task<Result<Issue>> Handle(GetIssueByIdQuery request, CancellationToken cancellationToken)
	{
		var backlog = await _backlogRepository.GetBacklogByIdWithIssuesAsync(request.BacklogId, cancellationToken);

		if (backlog is null)
		{
			_logger.LogWarning("Backlog not found by id {BacklogId}", request.BacklogId);

			return Result.Failure<Issue>(DomainError.Product.NotFoundProduct);
		}

		var issue = backlog.Issues.FirstOrDefault(x => x.Id == request.IssueId);

		if (issue is null)
		{
			_logger.LogWarning("Issue not found in backlog by id {IssueId}", request.IssueId);

			return Result.Failure<Issue>(DomainError.Product.NotFoundIssue);
		}

		return Result.Success(issue);
	}
}
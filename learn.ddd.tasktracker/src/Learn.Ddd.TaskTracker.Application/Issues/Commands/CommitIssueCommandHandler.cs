using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Application.Persistence;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Issues.Commands;

public record CommitIssueCommandHandler : IRequestHandler<CommitIssueCommand, Result>
{
	private readonly IBacklogRepository _backlogRepository;
	private readonly ILogger<CommitIssueCommandHandler> _logger;
	private readonly IUnitOfWork _ofWork;
	private readonly IProductRepository _productRepository;

	public CommitIssueCommandHandler(ILogger<CommitIssueCommandHandler> logger,
		IBacklogRepository backlogRepository,
		IProductRepository productRepository,
		IUnitOfWork ofWork)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_backlogRepository = backlogRepository ?? throw new ArgumentNullException(nameof(backlogRepository));
		_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
		_ofWork = ofWork ?? throw new ArgumentNullException(nameof(ofWork));
	}

	/// <inheritdoc />
	public async Task<Result> Handle(CommitIssueCommand request, CancellationToken cancellationToken)
	{
		var issue = await _backlogRepository.GetBacklogIssueByIdAsync(request.IssueId, cancellationToken);

		if (issue is null)
		{
			_logger.LogWarning("Not found issue by id {IssueId}", request.IssueId);

			return Result.Failure(DomainError.Product.NotFoundIssue);
		}

		var sprint = await _productRepository.GetSprintByIdAsync(request.SprintId, cancellationToken);

		if (sprint is null)
		{
			_logger.LogWarning("Not found sprint by id {SprintId}", request.SprintId);

			return Result.Failure(DomainError.Product.NotFoundSprint);
		}

		issue.Commit(issue.Id, sprint.Id);

		await _ofWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}
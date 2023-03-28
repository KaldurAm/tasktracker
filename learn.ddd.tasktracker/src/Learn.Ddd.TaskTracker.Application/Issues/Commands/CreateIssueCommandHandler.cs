using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Application.Persistence;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Issues.Commands;

public class CreateIssueCommandHandler : IRequestHandler<CreateIssueCommand, Result<Issue>>
{
	private readonly IBacklogRepository _backlogRepository;
	private readonly ILogger<CreateIssueCommandHandler> _logger;
	private readonly IUnitOfWork _ofWork;

	public CreateIssueCommandHandler(ILogger<CreateIssueCommandHandler> logger, IBacklogRepository backlogRepository, IUnitOfWork ofWork)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_backlogRepository = backlogRepository ?? throw new ArgumentNullException(nameof(backlogRepository));
		_ofWork = ofWork ?? throw new ArgumentNullException(nameof(ofWork));
	}

	/// <inheritdoc />
	public async Task<Result<Issue>> Handle(CreateIssueCommand request, CancellationToken cancellationToken)
	{
		var backlog = await _backlogRepository.GetBacklogByIdWithIssuesAsync(request.BacklogId, cancellationToken);

		if (backlog is null)
		{
			_logger.LogWarning("Backlog not found by id {BacklogId}", request.BacklogId);

			return Result.Failure<Issue>(DomainError.Product.NotFoundBacklog);
		}

		Issue issue = new(
			Guid.NewGuid(),
			request.BacklogId,
			request.Title,
			request.Description,
			request.PriorityId,
			request.TypeId,
			request.State,
			request.Estimation);

		backlog.CreateIssue(issue);

		await _ofWork.SaveChangesAsync(cancellationToken);

		backlog.AddIssue(issue);

		return Result.Success(issue);
	}
}
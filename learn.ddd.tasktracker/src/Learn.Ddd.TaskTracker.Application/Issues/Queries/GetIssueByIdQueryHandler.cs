using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Issues.Queries;

public record GetIssueByIdQueryHandler : IRequestHandler<GetIssueByIdQuery, Result<Issue>>
{
	private readonly IDataContext _context;
	private readonly ILogger<GetIssueByIdQueryHandler> _logger;

	public GetIssueByIdQueryHandler(ILogger<GetIssueByIdQueryHandler> logger, IDataContext context)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	/// <inheritdoc />
	public async Task<Result<Issue>> Handle(GetIssueByIdQuery request, CancellationToken cancellationToken)
	{
		var issue = await _context.Issues.FindAsync(request.IssueId, cancellationToken);

		if (issue is null)
		{
			_logger.LogWarning("Issue not found by id {IssueId}", request.IssueId);

			return Result.Failure<Issue>(DomainError.Product.NotFoundIssue);
		}

		return Result.Success(issue);
	}
}
using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Issues.Queries;

public record GetIssuesQueryHandler : IRequestHandler<GetIssuesQuery, Result<List<Issue>>>
{
	private readonly IDataContext _context;
	private readonly ILogger<GetIssuesQueryHandler> _logger;

	public GetIssuesQueryHandler(IDataContext context, ILogger<GetIssuesQueryHandler> logger)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
	}

	/// <inheritdoc />
	public async Task<Result<List<Issue>>> Handle(GetIssuesQuery request, CancellationToken cancellationToken)
	{
		var issues = await _context.Issues
			.OrderBy(issue => issue.CreatedAt)
			.Skip((request.PageNumber - 1) * request.PageSize)
			.Take(request.PageSize)
			.AsNoTracking()
			.ToListAsync(cancellationToken);

		if (!issues.Any())
		{
			_logger.LogWarning("Issue collection is null");

			return Result.Failure<List<Issue>>(DomainError.Product.NotFoundIssues);
		}

		return Result.Success(issues);
	}
}
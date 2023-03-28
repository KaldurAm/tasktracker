using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Issues.Queries;

public record GetIssuesQueryHandler : IRequestHandler<GetIssuesQuery, Result<List<Issue>>>
{
	private readonly ILogger<GetIssuesQueryHandler> _logger;

	/// <inheritdoc />
	public async Task<Result<List<Issue>>> Handle(GetIssuesQuery request, CancellationToken cancellationToken)
	{
		throw new NotImplementedException();
	}
}
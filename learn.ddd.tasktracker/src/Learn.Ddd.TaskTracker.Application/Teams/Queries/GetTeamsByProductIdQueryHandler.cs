using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Teams.Queries;

public record GetTeamsByProductIdQueryHandler : IRequestHandler<GetTeamsByProductIdQuery, Result<Team>>
{
	private readonly IDataContext _context;
	private readonly ILogger<GetTeamsByProductIdQueryHandler> _logger;

	public GetTeamsByProductIdQueryHandler(ILogger<GetTeamsByProductIdQueryHandler> logger, IDataContext context)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	/// <inheritdoc />
	public async Task<Result<Team>> Handle(GetTeamsByProductIdQuery request, CancellationToken cancellationToken)
	{
		var product = await _context.Products
			.Include(i => i.Team)
			.FirstOrDefaultAsync(f => f.Id == request.ProductId, cancellationToken);

		if (product is null)
		{
			_logger.LogWarning("Not found product by id {ProductId}", request.ProductId);

			return Result.Failure<Team>(DomainError.Product.NotFoundProduct);
		}

		if (product.Team is null)
		{
			_logger.LogWarning("Not found product by id {ProductId}", request.ProductId);

			return Result.Failure<Team>(DomainError.Product.NotFoundTeam);
		}

		return Result.Success(product.Team);
	}
}
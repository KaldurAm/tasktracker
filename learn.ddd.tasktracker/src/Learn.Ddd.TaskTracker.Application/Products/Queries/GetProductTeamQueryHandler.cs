using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Products.Queries;

public record GetProductTeamQueryHandler : IRequestHandler<GetProductTeamQuery, Result<Team>>
{
	private readonly ILogger<GetProductTeamQueryHandler> _logger;
	private readonly IProductRepository _productRepository;

	public GetProductTeamQueryHandler(ILogger<GetProductTeamQueryHandler> logger, IProductRepository productRepository)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
	}

	/// <inheritdoc />
	public async Task<Result<Team>> Handle(GetProductTeamQuery request, CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetProductByIdIncludeTeamAsync(request.productId, cancellationToken);

		if (product is null)
		{
			_logger.LogWarning("Product not found by id {ProductId}", request.productId);

			return Result.Failure<Team>(DomainError.Product.NotFoundProduct);
		}

		if (product.Team is null)
		{
			_logger.LogWarning("There is no backlog in product");

			return Result.Failure<Team>(DomainError.Product.NotFoundTeam);
		}

		return Result.Success(product.Team);
	}
}
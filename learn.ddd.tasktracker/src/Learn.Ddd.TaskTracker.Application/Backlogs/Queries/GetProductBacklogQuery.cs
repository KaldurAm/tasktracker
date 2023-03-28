using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Backlogs.Queries;

public class GetProductBacklogQuery : IRequestHandler<GetBacklogsByProductIdQuery, Result<Backlog>>
{
	private readonly ILogger<GetProductBacklogQuery> _logger;
	private readonly IProductRepository _productRepository;

	public GetProductBacklogQuery(ILogger<GetProductBacklogQuery> logger, IProductRepository productRepository)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
	}

	/// <inheritdoc />
	public async Task<Result<Backlog>> Handle(GetBacklogsByProductIdQuery request, CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetProductByIdIncludeBacklogAsync(request.ProductId, cancellationToken);

		if (product is null)
		{
			_logger.LogWarning("Not found product by id {ProductId}", request.ProductId);

			return Result.Failure<Backlog>(DomainError.Product.NotFoundProducts);
		}

		if (product.Backlog is null)
		{
			_logger.LogWarning("Not found product by id {ProductId}", request.ProductId);

			return Result.Failure<Backlog>(DomainError.Product.NotFoundBacklog);
		}

		return Result.Success(product.Backlog);
	}
}
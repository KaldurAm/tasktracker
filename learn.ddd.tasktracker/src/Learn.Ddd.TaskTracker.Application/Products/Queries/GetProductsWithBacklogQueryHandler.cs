using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Application.Products.Validators;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Products.Queries;

public record GetProductsWithBacklogQueryHandler : IRequestHandler<GetProductsWithBacklogQuery, Result<IEnumerable<Product>>>
{
	private readonly ILogger<GetProductsWithBacklogQueryHandler> _logger;
	private readonly IProductRepository _productRepository;

	public GetProductsWithBacklogQueryHandler(ILogger<GetProductsWithBacklogQueryHandler> logger, IProductRepository productRepository)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
	}

	/// <inheritdoc />
	public async Task<Result<IEnumerable<Product>>> Handle(GetProductsWithBacklogQuery request, CancellationToken cancellationToken)
	{
		var products = await _productRepository.GetProductsIncludeBacklogAsync(request.PageNumber, request.PageSize, cancellationToken);

		var productsList = products.ToList();

		if (productsList is null || !productsList.Any())
		{
			_logger.LogWarning("Not found products {Products}", productsList);
			return Result.Failure<IEnumerable<Product>>(DomainError.Product.NotFoundProducts);
		}

		return Result.Success<IEnumerable<Product>>(productsList);
	}
}
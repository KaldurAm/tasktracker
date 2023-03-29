using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Products.Queries;

public record GetProductDetailsQueryHandler : IRequestHandler<GetProductDetailsQuery, Result<Product>>
{
	private readonly ILogger<GetProductDetailsQueryHandler> _logger;
	private readonly IProductRepository _productRepository;

	public GetProductDetailsQueryHandler(ILogger<GetProductDetailsQueryHandler> logger, IProductRepository productRepository)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
	}

	/// <inheritdoc />
	public async Task<Result<Product>> Handle(GetProductDetailsQuery request, CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetProductByIdIncludeDetailsAsync(request.ProductId, cancellationToken);

		if (product is null)
		{
			_logger.LogWarning("Product not found by id {ProductId}", request.ProductId);

			return Result.Failure<Product>(DomainError.Product.NotFoundProduct);
		}

		_logger.LogInformation("Product found by id {ProductId}", request.ProductId);

		return Result.Success(product);
	}
}
using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Application.Products.Validators;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Products.Queries;

public record GetProductsQueryHandler : IRequestHandler<GetProductsQuery, Result<IEnumerable<Product>>>
{
	private readonly ILogger<GetProductsQueryHandler> _logger;
	private readonly IProductRepository _productRepository;

	public GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger, IProductRepository productRepository)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
	}

	/// <inheritdoc />
	public async Task<Result<IEnumerable<Product>>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
	{
		var products = await _productRepository.GetProductsAsync(request.PageNumber, request.PageSize, cancellationToken);

		return products.ValidateAndReturnResult();
	}
}
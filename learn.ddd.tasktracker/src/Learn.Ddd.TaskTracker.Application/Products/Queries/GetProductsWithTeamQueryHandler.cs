using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Application.Products.Validators;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Products.Queries;

public record GetProductsWithTeamQueryHandler : IRequestHandler<GetProductsWithTeamQuery, Result<IEnumerable<Product>>>
{
	private readonly IProductRepository _productRepository;

	public GetProductsWithTeamQueryHandler(IProductRepository productRepository)
	{
		_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
	}

	/// <inheritdoc />
	public async Task<Result<IEnumerable<Product>>> Handle(GetProductsWithTeamQuery request, CancellationToken cancellationToken)
	{
		var products = await _productRepository.GetProductsIncludeTeamAsync(request.PageNumber, request.PageSize, cancellationToken);

		return products.ValidateAndReturnResult();
	}
}
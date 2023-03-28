using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Application.Products.Validators;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Products.Queries;

public record GetProductQueryHandler : IRequestHandler<GetProductQuery, Result<Product>>
{
	private readonly IProductRepository _productRepository;

	public GetProductQueryHandler(IProductRepository productRepository) 
		=> _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));

	/// <inheritdoc />
	public async Task<Result<Product>> Handle(GetProductQuery request, CancellationToken cancellationToken)
	{
		var product = await _productRepository.GetProductByIdIncludeTeamAndBacklogAsync(request.Id, cancellationToken);

		return product.ValidateAndReturnResult();
	}
}
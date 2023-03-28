using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Application.Persistence;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Products.Commands;

public record CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Product>>
{
	private readonly ILogger<CreateProductCommandHandler> _logger;
	private readonly IUnitOfWork _ofWork;
	private readonly IProductRepository _productRepository;

	public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger, IProductRepository productRepository, IUnitOfWork ofWork)
	{
		_logger = logger ?? throw new ArgumentNullException(nameof(logger));
		_productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
		_ofWork = ofWork ?? throw new ArgumentNullException(nameof(ofWork));
	}

	/// <inheritdoc />
	public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
	{
		_logger.LogInformation("Start creating new product");

		var productId = Guid.NewGuid();

		var product = Product.Create(productId, request.Name, request.Description);

		await _productRepository.AddAsync(product, cancellationToken);

		_logger.LogInformation("Product added");

		product.AddTeam();

		_logger.LogInformation("Team added");

		product.AddBacklog();

		_logger.LogInformation("Backlog added");

		await _ofWork.SaveChangesAsync(cancellationToken);

		_logger.LogInformation("Changes commited");

		return Result.Success(product);
	}
}
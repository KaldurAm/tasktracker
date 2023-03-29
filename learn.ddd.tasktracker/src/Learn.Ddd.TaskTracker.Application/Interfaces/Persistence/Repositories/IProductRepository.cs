using Learn.Ddd.TaskTracker.Domain.Entities.Products;

namespace Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;

public interface IProductRepository
{
	Task AddAsync(Product product, CancellationToken cancellationToken = default);

	Task<Product?> GetProductByIdIncludeDetailsAsync(Guid id, CancellationToken cancellationToken = default);
	Task<Product?> GetProductByIdIncludeTeamAndBacklogAsync(Guid id, CancellationToken cancellationToken = default);
	Task<Product?> GetProductByIdIncludeTeamAsync(Guid id, CancellationToken cancellationToken = default);
	Task<Product?> GetProductByIdIncludeBacklogAsync(Guid id, CancellationToken cancellationToken = default);
	Task<Product?> GetProductByIdIncludeBacklogIssues(Guid id, CancellationToken cancellationToken = default);

	Task<IEnumerable<Product>> GetProductsAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
	Task<IEnumerable<Product>> GetProductsIncludeTeamAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
	Task<IEnumerable<Product>> GetProductsIncludeBacklogAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);
	Task<IEnumerable<Product>> GetProductsIncludeBacklogIssues(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default);

	Task<IEnumerable<Product>> GetProductsIncludeTeamAndBacklogAsync(int pageNumber = 1,
		int pageSize = 10,
		CancellationToken cancellationToken = default);

	Task<Sprint?> GetSprintByIdAsync(Guid sprintId, CancellationToken cancellationToken = default);
}
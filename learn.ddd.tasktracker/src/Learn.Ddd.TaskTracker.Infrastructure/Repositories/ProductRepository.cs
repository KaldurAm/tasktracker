using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Learn.Ddd.TaskTracker.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Learn.Ddd.TaskTracker.Infrastructure.Repositories;

public class ProductRepository : IProductRepository
{
	private readonly DataContext _context;

	public ProductRepository(DataContext context)
	{
		_context = context ?? throw new ArgumentNullException(nameof(context));
	}

	/// <inheritdoc />
	public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
	{
		await _context.Set<Product>()
			.AddAsync(product, cancellationToken);
	}

	/// <inheritdoc />
	public async Task<Product?> GetProductByIdIncludeDetailsAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _context.Set<Product>()
			.Where(product => product.Id == id)
			.Include(product => product.Team)
			.ThenInclude(team => team.Members.OrderBy(member => member.LastName))
			.Include(product => product.Backlog)
			.ThenInclude(backlog => backlog.Issues.OrderBy(issue => issue.CreatedAt))
			.AsNoTracking()
			.FirstOrDefaultAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task<Product?> GetProductByIdIncludeTeamAndBacklogAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _context.Set<Product>()
			.Where(x => x.Id == id)
			.Include(i => i.Team)
			.Include(i => i.Backlog)
			.FirstOrDefaultAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task<Product?> GetProductByIdIncludeTeamAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _context.Set<Product>()
			.Where(x => x.Id == id)
			.Include(i => i.Team)
			.FirstOrDefaultAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task<Product?> GetProductByIdIncludeBacklogAsync(Guid id, CancellationToken cancellationToken = default)
	{
		return await _context.Set<Product>()
			.Where(x => x.Id == id)
			.Include(i => i.Backlog)
			.FirstOrDefaultAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task<Product?> GetProductByIdIncludeBacklogIssues(Guid id, CancellationToken cancellationToken = default)
	{
		return await _context.Set<Product>()
			.Where(x => x.Id == id)
			.OrderBy(o => o.CreatedAt)
			.Include(i => i.Backlog)
			.ThenInclude(i => i!.Issues.OrderBy(o => o.CreatedAt))
			.FirstOrDefaultAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task<IEnumerable<Product>> GetProductsAsync(int pageNumber = 1, int pageSize = 10, CancellationToken cancellationToken = default)
	{
		return await _context.Set<Product>()
			.OrderBy(o => o.CreatedAt)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task<IEnumerable<Product>> GetProductsIncludeTeamAsync(int pageNumber = 1,
		int pageSize = 10,
		CancellationToken cancellationToken = default)
	{
		return await _context.Set<Product>()
			.OrderBy(o => o.CreatedAt)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.Include(i => i.Team)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task<IEnumerable<Product>> GetProductsIncludeBacklogAsync(int pageNumber = 1,
		int pageSize = 10,
		CancellationToken cancellationToken = default)
	{
		return await _context.Set<Product>()
			.OrderBy(o => o.CreatedAt)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.Include(i => i.Backlog)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task<IEnumerable<Product>> GetProductsIncludeBacklogIssues(int pageNumber = 1,
		int pageSize = 10,
		CancellationToken cancellationToken = default)
	{
		return await _context.Set<Product>()
			.OrderBy(o => o.CreatedAt)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.Include(i => i.Backlog)
			.ThenInclude(i => i!.Issues)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task<IEnumerable<Product>> GetProductsIncludeTeamAndBacklogAsync(int pageNumber = 1,
		int pageSize = 10,
		CancellationToken cancellationToken = default)
	{
		return await _context.Set<Product>()
			.OrderBy(o => o.CreatedAt)
			.Skip((pageNumber - 1) * pageSize)
			.Take(pageSize)
			.Include(i => i.Team)
			.Include(i => i.Backlog)
			.AsNoTracking()
			.ToListAsync(cancellationToken);
	}

	/// <inheritdoc />
	public async Task<Sprint?> GetSprintByIdAsync(Guid sprintId, CancellationToken cancellationToken = default)
	{
		return await _context.Set<Sprint>()
			.FirstOrDefaultAsync(sprint => sprint.Id == sprintId, cancellationToken);
	}
}
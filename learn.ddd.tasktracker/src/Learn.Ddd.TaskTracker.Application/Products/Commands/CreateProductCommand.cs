using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Products.Commands;

public record CreateProductCommand : IRequest<Result<Product>>
{
	public CreateProductCommand(string name, string? description = default)
	{
		Name = name;
		Description = description ?? string.Empty;
	}

	public string Name { get; }
	public string Description { get; }
}
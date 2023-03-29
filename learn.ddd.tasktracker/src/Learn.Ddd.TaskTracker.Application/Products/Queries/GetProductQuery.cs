using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Products.Queries;

public class GetProductQuery : IRequest<Result<Product>>
{
	public GetProductQuery(Guid id)
	{
		Id = id;
	}

	public Guid Id { get; }
}
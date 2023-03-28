using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Products.Queries;

public record GetProductsQuery(int PageNumber = 0, int PageSize = 10) : IRequest<Result<IEnumerable<Product>>>;
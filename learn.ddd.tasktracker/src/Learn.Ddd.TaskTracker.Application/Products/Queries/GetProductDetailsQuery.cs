using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Products.Queries;

public record GetProductDetailsQuery(Guid ProductId) : IRequest<Result<Product>>;
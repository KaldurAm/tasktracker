using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Backlogs.Queries;

public record GetBacklogsByProductIdQuery(Guid ProductId) : IRequest<Result<Backlog>>;
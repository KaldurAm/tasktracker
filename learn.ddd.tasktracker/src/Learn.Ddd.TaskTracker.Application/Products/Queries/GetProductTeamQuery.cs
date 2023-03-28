using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Products.Queries;

public record GetProductTeamQuery(Guid productId) : IRequest<Result<Team>>;
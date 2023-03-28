using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Teams.Queries;

public record GetTeamsByProductIdQuery(Guid ProductId) : IRequest<Result<Team>>;
using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Teams.Queries;

public record GetTeamWithMembersQuery(Guid TeamId) : IRequest<Result<Team>>;
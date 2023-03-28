using KDS.Primitives.FluentResult;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Teams.Commands;

public record AddMemberToTeamCommand(Guid TeamId, Guid MemberId) : IRequest<Result>;
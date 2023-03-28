using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Teams;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Members.Queries;

public record GetMemberByIdQuery(Guid Id) : IRequest<Result<TeamMember>>;
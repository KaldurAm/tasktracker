using KDS.Primitives.FluentResult;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Members.Commands;

public record CreateMemberCommand(string FirstName, string LastName, string Email) : IRequest<Result<Guid>>;
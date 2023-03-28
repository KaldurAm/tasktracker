using KDS.Primitives.FluentResult;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Issues.Commands;

public record CommitIssueCommand(Guid IssueId, Guid SprintId) : IRequest<Result>;
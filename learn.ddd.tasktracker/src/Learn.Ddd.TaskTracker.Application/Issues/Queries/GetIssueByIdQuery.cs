using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Issues.Queries;

public record GetIssueByIdQuery(Guid BacklogId, Guid IssueId, int Skip = 0, int Take = 10) : IRequest<Result<Issue>>;
using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Issues.Queries;

public record GetIssueByIdQuery(Guid IssueId, int pageNumber = 1, int PageSize = 10) : IRequest<Result<Issue>>;
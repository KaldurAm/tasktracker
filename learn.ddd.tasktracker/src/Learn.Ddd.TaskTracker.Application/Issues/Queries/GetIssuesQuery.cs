using KDS.Primitives.FluentResult;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;

namespace Learn.Ddd.TaskTracker.Application.Issues.Queries;

public record GetIssuesQuery(int PageNumber, int PageSize) : IRequest<Result<List<Issue>>>;
using AutoMapper;
using Learn.Ddd.TaskTracker.Api.Contracts.Responses;
using Learn.Ddd.TaskTracker.Application.Issues.Queries;
using MediatR;

namespace Learn.Ddd.TaskTracker.Api.Endpoints;

public static class IssuesEndpoints
{
	public static WebApplication AddIssuesEndpoints(this WebApplication app)
	{
		app.MapGet("api/issues",
			async (int pageNumber, int pageSize, ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new GetIssuesQuery(pageNumber, pageSize));
				
				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Ok(mapper.Map<List<IssueResponse>>(result.Value));
			});

		return app;
	}
}
using AutoMapper;
using Learn.Ddd.TaskTracker.Api.Contracts.Requests;
using Learn.Ddd.TaskTracker.Api.Contracts.Responses;
using Learn.Ddd.TaskTracker.Application.Backlogs.Queries;
using Learn.Ddd.TaskTracker.Application.Issues.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Learn.Ddd.TaskTracker.Api.Endpoints;

public static class BacklogsEndpoints
{
	public static WebApplication AddBacklogsEndpoints(this WebApplication app)
	{
		app.MapGet("api/backlogs/{backlogId}",
			async (Guid backlogId, ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new GetBacklogIssuesQuery(backlogId));

				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Ok(mapper.Map<IEnumerable<IssueResponse>>(result.Value));
			});

		app.MapPost("api/backlogs/create-issue",
			async ([FromBody] CreateIssueRequest createIssueRequest, ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new CreateIssueCommand(
					createIssueRequest.BacklogId,
					createIssueRequest.Title,
					createIssueRequest.Description,
					createIssueRequest.PriorityId,
					createIssueRequest.TypeId,
					createIssueRequest.State,
					createIssueRequest.Estimation));

				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				var mappedIssue = mapper.Map<IssueResponse>(result.Value);

				return Results.Created("api/issues/" + mappedIssue.Id, mappedIssue);
			});

		return app;
	}
}
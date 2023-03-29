using AutoMapper;
using Learn.Ddd.TaskTracker.Api.Contracts.Requests;
using Learn.Ddd.TaskTracker.Api.Contracts.Responses;
using Learn.Ddd.TaskTracker.Application.Products.Queries;
using Learn.Ddd.TaskTracker.Application.Teams.Commands;
using Learn.Ddd.TaskTracker.Application.Teams.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Learn.Ddd.TaskTracker.Api.Endpoints;

public static class TeamsEndpoints
{
	public static WebApplication AddTeamsEndpoints(this WebApplication app)
	{
		app.MapGet("api/teams/{productId}",
			async (Guid productId, ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new GetProductTeamQuery(productId));

				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Ok(mapper.Map<TeamResponse>(result.Value));
			});

		app.MapGet("api/teams/with-members",
			async (Guid teamId, ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new GetTeamWithMembersQuery(teamId));

				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Ok(mapper.Map<TeamWithMembersResponse>(result.Value));
			});

		app.MapPost("api/teams/add-member",
			async ([FromBody] AddMemberToTeamRequest request, ISender sender) =>
			{
				var result = await sender.Send(new AddMemberToTeamCommand(request.TeamId, request.MemberId));

				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Ok();
			});

		return app;
	}
}
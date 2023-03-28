using AutoMapper;
using Learn.Ddd.TaskTracker.Api.Contracts.Requests;
using Learn.Ddd.TaskTracker.Api.Contracts.Responses;
using Learn.Ddd.TaskTracker.Application.Members.Commands;
using Learn.Ddd.TaskTracker.Application.Members.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Learn.Ddd.TaskTracker.Api.Endpoints;

public static class MembersEndpoints
{
	public static WebApplication AddMembersEndpoints(this WebApplication app)
	{
		app.MapPost("api/members/create",
			async ([FromBody] CreateMemberRequest request, ISender sender) =>
			{
				var result = await sender.Send(new CreateMemberCommand(request.FirstName, request.LastName, request.Email));
    
				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);
    
				return Results.Ok(result.Value);
			});
	
		app.MapGet("api/members/{memberId}",
			async (Guid memberId, ISender sender, IMapper mapper) =>
			{
				var result = await sender.Send(new GetMemberByIdQuery(memberId));
			
				if (result.IsFailed)
					return Results.Problem(
						title : result.Error.Code,
						detail : result.Error.Message,
						statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

				return Results.Ok(mapper.Map<MemberResponse>(result.Value));
			});

		return app;
	}
}
using System.Net;
using System.Reflection;
using AutoMapper;
using Learn.Ddd.TaskTracker.Api.Contracts.Requests;
using Learn.Ddd.TaskTracker.Api.Contracts.Responses;
using Learn.Ddd.TaskTracker.Api.Providers;
using Learn.Ddd.TaskTracker.Application;
using Learn.Ddd.TaskTracker.Application.Backlogs.Queries;
using Learn.Ddd.TaskTracker.Application.Interfaces.Providers;
using Learn.Ddd.TaskTracker.Application.Issues.Commands;
using Learn.Ddd.TaskTracker.Application.Members.Commands;
using Learn.Ddd.TaskTracker.Application.Members.Queries;
using Learn.Ddd.TaskTracker.Application.Products.Commands;
using Learn.Ddd.TaskTracker.Application.Products.Queries;
using Learn.Ddd.TaskTracker.Application.Teams.Commands;
using Learn.Ddd.TaskTracker.Application.Teams.Queries;
using Learn.Ddd.TaskTracker.Infrastructure;
using Learn.Ddd.TaskTracker.Infrastructure.Persistence.Initializers;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

try
{
	var builder = WebApplication.CreateBuilder(args);

	Log.Logger = new LoggerConfiguration()
		.ReadFrom.Configuration(builder.Configuration)
		.CreateLogger();

	builder.Host.UseSerilog(Log.Logger);
	builder.Services.AddHttpContextAccessor();

	builder.Services.AddControllers()
		.AddNewtonsoftJson(options =>
			options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

	builder.Services.AddEndpointsApiExplorer();
	builder.Services.AddSwaggerGen();
	builder.Services.AddScoped<IDateTimeProvider, DateTimeProvider>();
	builder.Services.AddScoped<IUserProvider, UserProvider>();
	builder.Services.AddScoped<ICorrelationProvider, CorrelationProvider>();
	builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

	builder.Services.AddApplication(builder.Configuration)
		.AddInfrastructure(builder.Configuration);

	builder.Services.AddCors(options =>
	{
		options.AddPolicy("web",
			policy =>
			{
				policy.WithOrigins(builder.Configuration["AllowedHosts"])
					.AllowAnyMethod()
					.AllowAnyHeader();
			});
	});

	var app = builder.Build();
	app.UseHttpsRedirection();
	app.UseSerilogRequestLogging();
	app.UseSwagger();
	app.UseSwaggerUI();
	app.Use(async (context, next) =>
	{
		try
		{
			await next(context);
		}
		catch(Exception ex)
		{
			Log.Logger.Error(ex, "Error occured while processing request");
			
			var problem = new ProblemDetails
			{
				Title = "Error occured while processing request",
				Detail = ex.Message,
				Status = (int)HttpStatusCode.InternalServerError,
				Instance = Assembly.GetExecutingAssembly().GetName().Name,
			};

			context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
			await context.Response.WriteAsJsonAsync(problem);
		}
		
	});
	app.UseAuthentication();
	app.UseAuthorization();
	app.MapControllers();
	app.UseCors("web");
	await Seeder.StartAsync(app);

	#region products

	app.MapGet("api/products",
		async (int pageNumber, int pageSize, ISender sender, IMapper mapper) =>
		{
			var result = await sender.Send(new GetProductsQuery(pageNumber, pageSize));

			if (result.IsFailed)
				return Results.Problem(
					title : result.Error.Code,
					detail : result.Error.Message,
					statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

			return Results.Ok(mapper.Map<IEnumerable<ProductResponse>>(result.Value));
		});

	app.MapGet("api/products/{productId}",
		async (Guid productId, ISender sender, IMapper mapper) =>
		{
			var result = await sender.Send(new GetProductQuery(productId));

			if (result.IsFailed)
				return Results.Problem(
					title : result.Error.Code,
					detail : result.Error.Message,
					statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

			return Results.Ok(mapper.Map<ProductResponse>(result.Value));
		});
	
	app.MapGet("api/products/details/{productId}",
		async (Guid productId, ISender sender, IMapper mapper) =>
		{
			var result = await sender.Send(new GetProductDetailsQuery(productId));

			if (result.IsFailed)
				return Results.Problem(
					title : result.Error.Code,
					detail : result.Error.Message,
					statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);
			
			return Results.Ok(mapper.Map<ProductWithDetails>(result.Value));
		});

	app.MapGet("api/products/with-team",
		async (ISender sender, IMapper mapper) =>
		{
			var result = await sender.Send(new GetProductsWithTeamQuery());

			if (result.IsFailed)
				return Results.Problem(
					title : result.Error.Code,
					detail : result.Error.Message,
					statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

			return Results.Ok(mapper.Map<IEnumerable<ProductWithTeamResponse>>(result.Value));
		});

	app.MapGet("api/products/with-backlog",
		async (int pageNumber, int pageSize, ISender sender, IMapper mapper) =>
		{
			var result = await sender.Send(new GetProductsWithBacklogQuery(pageNumber, pageSize));
			
			if (result.IsFailed)
				return Results.Problem(
					title : result.Error.Code,
					detail : result.Error.Message,
					statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);
			
			return Results.Ok(mapper.Map<IEnumerable<ProductWithBacklogResponse>>(result.Value));
		});

	app.MapGet("api/products/with-team-and-backlog",
		async (int pageNumber, int pageSize, ISender sender, IMapper mapper) =>
		{
			var result = await sender.Send(new GetProductsWithTeamAndBacklogQuery(pageNumber, pageSize));

			if (result.IsFailed)
				return Results.Problem(
					title : result.Error.Code,
					detail : result.Error.Message,
					statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

			return Results.Ok(mapper.Map<IEnumerable<ProductWithTeamAndBacklogResponse>>(result.Value));
		});

	app.MapPost("api/products/create",
		async ([FromBody] CreateProductRequest request, ISender sender, IMapper mapper) =>
		{
			var result = await sender.Send(new CreateProductCommand(request.Name, request.Description));

			if (result.IsFailed)
				return Results.Problem(title : result.Error.Code,
					detail : result.Error.Message,
					statusCode : result.Error.Code.Equals("NOT_FOUND") ? 404 : 500);

			return Results.Created("api/products/" + result.Value.Id, mapper.Map<ProductResponse>(result.Value));
		});

	#endregion products

	#region backlogs

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
	
	#endregion backlogs

	#region issues

	app.MapGet("api/issues",
		async (int pageNumber, int pageSize, ISender sender, IMapper mapper) => { });
	
	#endregion
	
	#region teams

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
	
	#endregion teams
	
	#region members
	
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
	
	#endregion members
	
	app.Run();
}
catch(Exception ex)
{
	Log.Logger.Error(ex, "Error occure while building application");
}
finally
{
	Log.Logger.Warning("Close and flush logger");
	Log.CloseAndFlush();
}
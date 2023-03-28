using System.Net;
using System.Reflection;
using AutoMapper;
using Learn.Ddd.TaskTracker.Api.Contracts.Requests;
using Learn.Ddd.TaskTracker.Api.Contracts.Responses;
using Learn.Ddd.TaskTracker.Api.Endpoints;
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
	app.AddProductsEndpoints()
		.AddBacklogsEndpoints()
		.AddIssuesEndpoints()
		.AddTeamsEndpoints()
		.AddMembersEndpoints();
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
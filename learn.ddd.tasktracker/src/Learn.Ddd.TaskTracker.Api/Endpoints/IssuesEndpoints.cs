using AutoMapper;
using MediatR;

namespace Learn.Ddd.TaskTracker.Api.Endpoints;

public static class IssuesEndpoints
{
	public static WebApplication AddIssuesEndpoints(this WebApplication app)
	{
		app.MapGet("api/issues",
			async (int pageNumber, int pageSize, ISender sender, IMapper mapper) => { });
		
		return app;
	}
}
using System.Reflection;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Learn.Ddd.TaskTracker.Application;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddMediatR(Assembly.GetExecutingAssembly());
		services.AddAutoMapper(Assembly.GetExecutingAssembly());

		return services;
	}
}
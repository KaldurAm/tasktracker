using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Application.Persistence;
using Learn.Ddd.TaskTracker.Infrastructure.Backgrounds;
using Learn.Ddd.TaskTracker.Infrastructure.Persistence;
using Learn.Ddd.TaskTracker.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Learn.Ddd.TaskTracker.Infrastructure;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<DataContext>(options =>
		{
			var connectionString = configuration.GetConnectionString("Postgres");
			
			options.UseNpgsql(connectionString, opt =>
			{
				opt.EnableRetryOnFailure(3);
				opt.CommandTimeout(30);
			});

			options.EnableDetailedErrors();
			options.EnableSensitiveDataLogging();
		});

		services.AddRepositories();
		services.AddBackgroundServices();

		return services;
	}

	private static void AddBackgroundServices(this IServiceCollection services)
	{
		services.AddHostedService<ProcessDomainEvent>();
	}

	private static void AddRepositories(this IServiceCollection services)
	{
		services.AddScoped<IDataContext, DataContext>();
		services.AddScoped<IProductRepository, ProductRepository>();
		services.AddScoped<IBacklogRepository, BacklogRepository>();
		services.AddScoped<ITeamRepository, TeamRepository>();
		services.AddScoped<IMemberRepository, MemberRepository>();
		services.AddScoped<IUnitOfWork, UnitOfWork>();
	}
}
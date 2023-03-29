using Learn.Ddd.TaskTracker.Domain.Errors.DomainEvents;
using Learn.Ddd.TaskTracker.Infrastructure.Persistence;
using Learn.Ddd.TaskTracker.Infrastructure.Tables;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Learn.Ddd.TaskTracker.Infrastructure.Backgrounds;

public class ProcessDomainEvent : BackgroundService
{
	private readonly IServiceProvider _serviceProvider;

	/// <inheritdoc />
	public ProcessDomainEvent(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
	}

	/// <inheritdoc />
	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			using var scope = _serviceProvider.CreateScope();

			var context = scope.ServiceProvider.GetRequiredService<DataContext>();

			var publisher = scope.ServiceProvider.GetRequiredService<IPublisher>();

			var logger = scope.ServiceProvider.GetRequiredService<ILogger<ProcessDomainEvent>>();

			var messages = await context.Set<OutboxMessage>()
				.Where(om => om.ProcessedOnUtc.Equals(null))
				.Take(20)
				.ToListAsync(stoppingToken);

			foreach(var message in messages)
			{
				var domainEvent = JsonConvert
					.DeserializeObject<IDomainEvent>(
						message.Content,
						new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All, });

				if (domainEvent is null)
					continue;

				try
				{
					await publisher.Publish(domainEvent, stoppingToken);

					message.ProcessedOnUtc = DateTime.UtcNow;
				}
				catch(Exception ex)
				{
					logger.LogError(ex, "Error occured while handling notification");

					continue;
				}
			}

			if (messages.Any())
			{
				await context.SaveChangesAsync(stoppingToken);
			}
		}
	}
}
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence;
using Learn.Ddd.TaskTracker.Domain.DomainEvents;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Learn.Ddd.TaskTracker.Application.Backlogs.Events;

public record CreateBacklogDomainEventHandler : INotificationHandler<CreateBacklogDomainEvent>
{
	private readonly IServiceProvider _serviceProvider;

	public CreateBacklogDomainEventHandler(IServiceProvider serviceProvider)
	{
		_serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
	}

	/// <inheritdoc />
	public async Task Handle(CreateBacklogDomainEvent notification, CancellationToken cancellationToken)
	{
		using var scope = _serviceProvider.CreateScope();

		var context = scope.ServiceProvider.GetRequiredService<IDataContext>();

		var logger = scope.ServiceProvider.GetRequiredService<ILogger<CreateBacklogDomainEventHandler>>();

		var product = await context.Products.FirstOrDefaultAsync(product => product.Id == notification.ProductId, cancellationToken);

		if (product is null)
		{
			logger.LogWarning("Product not found by id {ProductId}", notification.ProductId);

			return;
		}

		var backlog = Backlog.Create(Guid.NewGuid(), product.Id, product.Name + " backlog");

		await context.Backlogs.AddAsync(backlog, cancellationToken);

		await context.SaveChangesAsync(cancellationToken);

		logger.LogInformation("Backlog {BacklogName} added to product {ProductName}", backlog.Name, product.Name);
	}
}